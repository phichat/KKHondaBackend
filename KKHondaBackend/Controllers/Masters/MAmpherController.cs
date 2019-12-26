using System.Collections.Generic;
using KKHondaBackend.Data;
using KKHondaBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KKHondaBackend.Controllers.Master
{
  [ApiController]
  [Produces("application/json")]
  [Route("api/Master/[controller]")]
  public class MAmpherController : Controller
  {
    private readonly dbwebContext ctx;

    public MAmpherController(
        dbwebContext _ctx
    )
    {
      ctx = _ctx;
    }

    [HttpGet("[Action]")]
    public async Task<IActionResult> GetAmpherByProvinceCode(string provinceCode)
    {
      return Ok(await ctx.MAmphor.Where(x => x.ProvinceCode == provinceCode).ToListAsync());
    }

    [HttpGet("[Action]")]
    public async Task<IActionResult> GetAmpherByCode(string ampherCode)
    {
      return Ok(await ctx.MAmphor.Where(x => x.AmphorCode == ampherCode).SingleAsync());
    }
  }
}