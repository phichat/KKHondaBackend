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
  // [ApiController]
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

    [HttpGet("[Action]")]
    public async Task<IActionResult> GetLeasingByBranch(int branchId)
    {
      var financeComList = await (from fcl in ctx.FinanceComList
                                  join minCt in ctx.CreditTermList on fcl.MinCtId equals minCt.CtId
                                  join maxCt in ctx.CreditTermList on fcl.MaxCtId equals maxCt.CtId
                                  select new
                                  {
                                    FicomId = fcl.FicomId,
                                    FiId = fcl.FiId,
                                    FiintId = fcl.FiintId,
                                    MinCtId = fcl.MinCtId,
                                    MaxCtId = fcl.MaxCtId,
                                    MinDown = fcl.MinDown,
                                    MaxDown = fcl.MaxDown,
                                    ComPrice = fcl.ComPrice,
                                    MinCtNo = minCt.CtNo,
                                    MaxCtNo = maxCt.CtNo
                                  }).ToListAsync();

      var list = await (from fi in ctx.FinanceList
                        join cus in iCust.GetLeasing on fi.FiCode equals cus.CustomerCode
                        where fi.FiStatus == 1 && fi.BranchId == branchId
                        select new
                        {
                          FiId = fi.FiId,
                          LeasingCode = cus.CustomerCode,
                          LeasingName = $"{cus.CustomerPrename}{cus.CustomerName}",
                          LeasingIntList = ctx.FinanceIntList
                            .Select(fil => new {
                              FiintId = fil.FiintId,
                              FiId = fil.FiId,
                              FiintNo = fil.FiintNo,
                              LeasingComList = financeComList
                                .Where(fcl => fcl.FiId == fi.FiId && fcl.FiintId == fil.FiintId)
                            })
                            .Where(fil => fil.FiId == fi.FiId)
                        }).ToListAsync();

      return Ok(list);
    }

  }
}
