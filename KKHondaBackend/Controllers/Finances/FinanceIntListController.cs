
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
  [ApiController]
  [Produces("application/json")]
  [Route("api/Master/[controller]")]
  public class FinanceIntListController : Controller
  {
    private readonly dbwebContext ctx;

    public FinanceIntListController(
        dbwebContext _ctx
    )
    {
      ctx = _ctx;
    }

    [HttpGet("[Action]")]
    public async Task<IActionResult> GetFinanceRateByFiId(int fiId)
    {
      var list = await ctx.FinanceIntList
      .Where(x => x.FiId == fiId)
      .Select(x => new
      {
        FiintNo = x.FiintNo
      }).ToListAsync();

      return Ok(list);
    }
  }
}