using System;
using System.Collections.Generic;
using System.Linq;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KKHondaBackend.Services
{
  public interface ICustomerServices
  {
    Dropdown[] GetDropdownByKey(string term);

    Dropdown[] GetDropdowns();

    MCustomer GetCustomerByCode(string custCode);

    IEnumerable<Customer> GetCustomer();
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

    public MCustomer GetCustomerByCode(string custCode)
    {
      var address = ctx.MCustomerAddress.Where(x => x.CustomerCode == custCode).ToList();
      var card = ctx.MCustomerCard.Where(x => x.CustomerCode == custCode).ToList();
      var customer = (from db in ctx.MCustomer
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
                      }).SingleOrDefault();
      return customer;
    }

    public Dropdown[] GetDropdownByKey(string term)
    {
      List<Dropdown> customerDropdowns = new List<Dropdown>();

      customerDropdowns = ctx.MCustomer
                             .Where(o => o.CustomerCode.Contains(term) || 
                             ($"{o.CustomerPrename}{o.CustomerName} {o.CustomerSurname}").Contains(term)
                             ).Select(o => new Dropdown
                             {
                               Value = o.CustomerCode,
                               Text = $"{o.CustomerPrename}{o.CustomerName} {o.CustomerSurname}"
                             }).Take(50).ToList();

      return customerDropdowns.ToArray();
    }

    public Dropdown[] GetDropdowns()
    {
      List<Dropdown> customerDropdowns = new List<Dropdown>();

      customerDropdowns = ctx.MCustomer
                             .Select(o => new Dropdown
                             {
                               Value = o.CustomerCode,
                               Text = $"{o.CustomerPrename}{o.CustomerName} {o.CustomerSurname}"
                             }).Take(50).ToList();

      return customerDropdowns.ToArray();
    }

    public IEnumerable<Customer> GetCustomer()
    {
      return ctx.MCustomer.Select(o => new Customer
      {
        CustomerCode = o.CustomerCode,
        CustomerFullName = $"{o.CustomerPrename}{o.CustomerName} {o.CustomerSurname}"
      }).AsNoTracking().ToList();
    }
  }

}
