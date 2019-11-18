using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using KKHondaBackend.Services;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KKHondaBackend.Controllers.Customers
{
  [Produces("application/json")]
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
    public async Task<IActionResult> GetAction()
    {
      var customer = await (from h in ctx.MCustomer
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

                      }).ToListAsync();

      return Ok(customer);
    }

    // GET
    [HttpGet("GetByKey")]
    public async Task<IActionResult> GetByKey(string term)
    {
      if (term == null)
        return NoContent();

      return Ok(await iCust.GetDropdownByKey(term));
    }

    [HttpGet("GetCustomerByCode")]
    public async Task<IActionResult> GetCustomerByCode(string custCode)
    {
      if (custCode == null)
        return NoContent();

      return Ok(await iCust.GetCustomerByCode(custCode));
    }

  }
}
