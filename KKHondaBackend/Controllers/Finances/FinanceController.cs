
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using KKHondaBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KKHondaBackend.Controllers.Finances
{
  // [ApiController]
  [Produces("application/json")]
  [Route("api/Finances/[controller]")]
  public class FinanceControll : Controller
  {
    private readonly dbwebContext ctx;
    private ICustomerServices iCust;

    public FinanceControll(
        dbwebContext _ctx, ICustomerServices iCustService
    )
    {
      ctx = _ctx;
      iCust = iCustService;
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
                          LeasingName = $"{cus.CustomerPrename}{cus.CustomerName} {cus.CustomerSurname}",
                          LeasingIntList = ctx.FinanceIntList.Where(fil => fil.FiId == fi.FiId),
                          LeasingComList = financeComList.Where(fcl => fcl.FiId == fi.FiId)
                        }).ToListAsync();

      return Ok(list);
    }
  }
}


