using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using KKHondaBackend.Data;
using KKHondaBackend.Models;

namespace KKHondaBackend.Services
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomerServices : ICustomerServices
    {
        private readonly dbwebContext ctx;

        public CustomerServices(dbwebContext context)
        {
            ctx = context;
        }

        public CustomerDropdown[] GetCustomerDropdownByKey(string term)
        {
            List<CustomerDropdown> customerDropdowns = new List<CustomerDropdown>();

            customerDropdowns = ctx.MCustomer
                                   .Where(o => o.CustomerCode.Contains(term) || o.CustomerName.Contains(term) ||
                                          o.CustomerSurname.Contains(term) || o.CustomerPrename.Contains(term))
                                   .Select(o => new CustomerDropdown
                                     {
                                         CustomerCode = o.CustomerCode,
                                         CustomerFullName = o.CustomerPrename + " " + o.CustomerName + " " + o.CustomerSurname
                                     }).Take(100).ToList();

            return customerDropdowns.ToArray();
        }

        public CustomerDropdown[] GetCustomerTop100Dropdowns() {
            List<CustomerDropdown> customerDropdowns = new List<CustomerDropdown>();

            customerDropdowns = ctx.MCustomer
                                   .Select(o => new CustomerDropdown
                                   {
                                       CustomerCode = o.CustomerCode,
                                       CustomerFullName = o.CustomerPrename + " " + o.CustomerName + " " + o.CustomerSurname
                                   }).Take(100).ToList();

            return customerDropdowns.ToArray();
        }
    }

}
