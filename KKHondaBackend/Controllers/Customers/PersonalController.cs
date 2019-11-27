using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KKHondaBackend.Controllers.Customers
{
  [ApiController]
  [Produces("application/json")]
  [Route("api/Customers/[controller]")]
  public class PersonalController : Controller
  {
    private readonly dbwebContext ctx;

    public PersonalController(dbwebContext context)
    {
      ctx = context;
    }

    // GET: api/values
    [HttpGet]
    public IActionResult Get()
    {
      var customer = (from h in ctx.MCustomer
                      join c in ctx.MCustomerCard on h.CustomerCode equals c.CustomerCode into a
                      from b in a.DefaultIfEmpty()
                      join ad in ctx.MCustomerAddress on h.CustomerCode equals ad.CustomerCode into a1
                      from b1 in a1.DefaultIfEmpty()
                      where h.TypePersonal.Equals("Y")
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

    // GET api/values/5
    [HttpGet("FilterByKey")]
    public IActionResult FilterByKey(string term)
    {
      var customer = (from h in ctx.MCustomer
                      join c in ctx.MCustomerCard on h.CustomerCode equals c.CustomerCode into a
                      from b in a.DefaultIfEmpty()
                      join ad in ctx.MCustomerAddress on h.CustomerCode equals ad.CustomerCode into a1
                      from b1 in a1.DefaultIfEmpty()
                      where h.TypePersonal.Equals("Y") &&
                      (
                          h.CustomerPrename.Contains(term) || h.CustomerName.Contains(term) ||
                          h.CustomerSurname.Contains(term) || h.CustomerCode.Contains(term) ||
                          b.CardId.Contains(term)
                      )
                      select new
                      {
                        cardId = b.CardId,
                        custCode = h.CustomerCode,
                        custFullName = h.CustomerPrename + " " + h.CustomerName + " " + h.CustomerSurname,
                        custTel = h.CustomerPhone,
                        custEmail = h.CustomerEmail,
                        custAddress = b1.Address + " " + b1.AmphorCode + " " + b1.ProvinceCode + " " + b1.Zipcode

                      }).Take(100).ToList();

      return Ok(customer);
    }


  }
}
