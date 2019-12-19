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
  public class CreditTermListController : Controller
  {
    private readonly dbwebContext ctx;

    public CreditTermListController(
        dbwebContext _ctx
    )
    {
      ctx = _ctx;
    }

    [HttpGet("[Action]")]
    public async Task<IActionResult> GetDropdown()
    {
      var list = await ctx.CreditTermList
      .Select(x => new Dropdown {
        Value = x.CtNo.ToString(),
        Text = x.CtNo.ToString()
      }).ToListAsync();

      return Ok(list);
    }
  }
}