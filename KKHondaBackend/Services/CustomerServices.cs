﻿using System;
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

        public Dropdown[] GetDropdownByKey(string term)
        {
            List<Dropdown> customerDropdowns = new List<Dropdown>();

            customerDropdowns = ctx.MCustomer
                                   .Where(o => o.CustomerCode.Contains(term) || o.CustomerName.Contains(term) ||
                                          o.CustomerSurname.Contains(term) || o.CustomerPrename.Contains(term))
                                   .Select(o => new Dropdown
                                   {
                                       Value = o.CustomerCode,
                                       Text = o.CustomerPrename + " " + o.CustomerName + " " + o.CustomerSurname
                                   }).Take(50).ToList();

            return customerDropdowns.ToArray();
        }

        public Dropdown[] GetDropdowns() {
            List<Dropdown> customerDropdowns = new List<Dropdown>();

            customerDropdowns = ctx.MCustomer
                                   .Select(o => new Dropdown
                                   {
                                       Value = o.CustomerCode,
                                       Text = o.CustomerPrename + " " + o.CustomerName + " " + o.CustomerSurname
                                   }).Take(50).ToList();

            return customerDropdowns.ToArray();
        }
    }

}
