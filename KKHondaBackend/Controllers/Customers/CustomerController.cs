using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using KKHondaBackend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KKHondaBackend.Controllers.Customers
{
    [Route("api/Customers/[controller]")]
    public class CustomerController : Controller
    {
        private readonly dbwebContext ctx;
        private ICustomerServices iCust;

        public CustomerController(dbwebContext context, ICustomerServices iCustService)
        {
            ctx = context;
            iCust = iCustService;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult GetAction()
        {
            var customer = (from h in ctx.MCustomer
                            join c in ctx.MCustomerCard on h.CustomerCode equals c.CustomerCode into a
                            from b in a.DefaultIfEmpty()
                            join ad in ctx.MCustomerAddress on h.CustomerCode equals ad.CustomerCode into a1
                            from b1 in a1.DefaultIfEmpty()
                                //where h.TypePersonal.Equals("Y")
                            select new
                            {
                                cardId = b.CardId,
                                custCode = h.CustomerCode,
                                custFullName = h.CustomerPrename + " " + h.CustomerName + " " + h.CustomerSurname,
                                custTel = h.CustomerPhone,
                                custEmail = h.CustomerEmail,
                                custAddress = b1.Address + " " + b1.AmphorCode + " " + b1.ProvinceCode + " " + b1.Zipcode

                            }).ToList();

            return Ok(customer);
        }

        // GET
        [HttpGet("GetByKey")]
        public IActionResult GetByKey(string term)
        {
            if (term == null)
                return NoContent();
            
            return Ok(iCust.GetDropdownByKey(term));
        }

        //public CustomerDropdown GetAllCustomerDropdown(){
        //    var customer = (from db in ctx.MCustomer
        //                    select new CustomerDropdown
        //                    {
        //                        CustomerCode = db.CustomerCode,
        //                        CustomerFullName = db.CustomerPrename + " " + db.CustomerName + " " + db.CustomerSurname
        //                    }).ToList();
        //    return Json(customer);
        //}

        //public class CustomerDropdown {
        //    public string CustomerCode { get; set; }
        //    public string CustomerFullName { get; set; }
        //}
            
    }
}
