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
                              IdCard = db.IdCard,
                              CreateBy = db.CreateBy,
                              CreateDate = db.CreateDate,
                              UpdateBy = db.UpdateBy,
                              UpdateDate = db.UpdateDate,
                              MCustomerAddress = address,
                              MCustomerCard = card
                            }).SingleOrDefaultAsync();

      if (customer == null)
      {
        customer = await ctx.Company.Where(x => x.ComCode == custCode)
        .Select(x => new MCustomer
        {
          CustomerCode = x.ComCode,
          CustomerName = x.ComName,
          IdCard = x.TaxId,
          TypePersonal = x.TypePersonal,
          CustomerPhone = x.Phone,
          MCustomerAddress = new List<MCustomerAddress> {
            new MCustomerAddress { CustomerCode = x.ComCode, Address = x.Address }
          }
        }).FirstOrDefaultAsync();
      }

      if (customer == null)
      {
        customer = await ctx.FinanceCompany.Where(x => x.FicCode == custCode)
        .Select(x => new MCustomer
        {
          CustomerCode = x.FicCode,
          CustomerName = x.FicName,
          IdCard = x.TaxId,
          TypePersonal = x.TypePersonal,
          CustomerPhone = x.Phone,
          MCustomerAddress = new List<MCustomerAddress> {
            new MCustomerAddress { CustomerCode = x.FicCode, Address = x.Address }
          }
        }).FirstOrDefaultAsync();
      }

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

      // if (customerDropdowns == null)
      // {
      //   customerDropdowns = await ctx.Company
      //     .Where(o => o.ComCode.Contains(term) || o.ComName.Contains(term))
      //     .Select(o => new Dropdown
      //     {
      //       Value = o.ComCode,
      //       Text = o.ComName
      //     })
      //     .Take(50)
      //     .ToListAsync();
      // }

      // if (customerDropdowns == null)
      // {
      //   customerDropdowns = await ctx.FinanceCompany
      //     .Where(o => o.FicCode.Contains(term) || o.FicName.Contains(term))
      //     .Select(o => new Dropdown
      //     {
      //       Value = o.FicCode,
      //       Text = o.FicName
      //     })
      //     .Take(50)
      //     .ToListAsync();
      // }

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
