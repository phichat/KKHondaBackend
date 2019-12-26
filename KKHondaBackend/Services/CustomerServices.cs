using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KKHondaBackend.Services
{
  public interface ICustomerServices
  {
    Task<IEnumerable<Dropdown>> GetDropdownByKey(string term);
    Task<IEnumerable<Dropdown>> GetDropdowns();
    Task<MCustomer> GetCustomerByCode(string custCode);
    Task<IEnumerable<Customer>> GetCustomer();
    IEnumerable<MCustomer> GetLeasing { get; }
  }

  public class Customer
  {
    public string CustomerCode { get; set; }
    public string CustomerFullName { get; set; }
  }

  // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
  public class CustomerServices : ICustomerServices
  {
    private readonly dbwebContext ctx;

    public CustomerServices(dbwebContext context)
    {
      ctx = context;
    }

    public async Task<MCustomer> GetCustomerByCode(string custCode)
    {

      var address = await ctx.MCustomerAddress.Where(x => x.CustomerCode == custCode).ToListAsync();
      var card = await ctx.MCustomerCard.Where(x => x.CustomerCode == custCode).ToListAsync();
      var customer = await (from db in ctx.MCustomer
                            where db.CustomerCode == custCode
                            select new MCustomer
                            {
                              CustomerCode = db.CustomerCode,
                              CustomerPrename = db.CustomerPrename,
                              CustomerName = db.CustomerName,
                              CustomerSurname = db.CustomerSurname,
                              CustomerNickname = db.CustomerNickname,
                              CustomerLevel = db.CustomerLevel,
                              CustomerPhone = db.CustomerPhone,
                              CustomerEmail = db.CustomerEmail,
                              CustomerSex = db.CustomerSex,
                              Birthday = db.Birthday,
                              Nationality = db.Nationality,
                              Occupation = db.Occupation,
                              EmergencyContactName = db.EmergencyContactName,
                              EmergencyContactPhone = db.EmergencyContactPhone,
                              TypePersonal = db.TypePersonal,
                              TypeCorporate = db.TypeCorporate,
                              TypeDealer = db.TypeDealer,
                              TypeSupplier = db.TypeSupplier,
                              TypeOther = db.TypeOther,
                              TypeFinance = db.TypeFinance,
                              IdCard = db.IdCard,
                              CreateBy = db.CreateBy,
                              CreateDate = db.CreateDate,
                              UpdateBy = db.UpdateBy,
                              UpdateDate = db.UpdateDate,
                              MCustomerAddress = address,
                              MCustomerCard = card
                            }).SingleOrDefaultAsync();

      return customer;
    }

    public async Task<IEnumerable<Dropdown>> GetDropdownByKey(string term)
    {
      var customerDropdowns = new List<Dropdown>();
      var fullName = new StringBuilder();
      var p1 = new SqlParameter("@term", term);
      var sqlText = @"select top 50 customer_code, customer_prename, customer_name, customer_surname 
      from dbo.m_customer 
      where customer_code like '%'+@term+'%' or
      CONCAT(customer_prename, customer_name, ' ', customer_surname) like '%'+@term+'%'";
      customerDropdowns = await ctx.MCustomer.FromSql(sqlText, p1)
      .Select(o => new Dropdown
      {
        Value = o.CustomerCode,
        Text = $"{o.CustomerPrename}{o.CustomerName} {o.CustomerSurname}"
      })
      .ToListAsync();

      return customerDropdowns;
    }

    public async Task<IEnumerable<Dropdown>> GetDropdowns()
    {
      List<Dropdown> customerDropdowns = new List<Dropdown>();

      customerDropdowns = await ctx.MCustomer
                             .Select(o => new Dropdown
                             {
                               Value = o.CustomerCode,
                               Text = string.Concat(o.CustomerPrename, o.CustomerName, " ", o.CustomerSurname)
                             })
                             .Take(50)
                             .ToListAsync();

      return customerDropdowns;
    }

    public IEnumerable<MCustomer> GetLeasing
    {
      get => ctx.MCustomer.Where(x => x.TypeFinance == true);
    }

    public async Task<IEnumerable<Customer>> GetCustomer()
    {
      return await ctx.MCustomer.Select(o => new Customer
      {
        CustomerCode = o.CustomerCode,
        CustomerFullName = string.Concat(o.CustomerPrename, o.CustomerName, " ", o.CustomerSurname)
      }).AsNoTracking().ToListAsync();
    }
  }

}
