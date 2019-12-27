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
  public class MProvinceController : Controller
  {
    private readonly dbwebContext ctx;

    public MProvinceController(
        dbwebContext _ctx
    )
    {
      ctx = _ctx;
    }

    [HttpGet("DropDown")]
    public IActionResult DropDown()
    {
      var dd = ctx.MProvince.Select(x => new Dropdown
      {
        Value = x.ProvinceCode,
        Text = x.ProvinceNameTh
      });
      return Ok(dd);
    }

    [HttpGet("[Action]")]
    public async Task<IActionResult> GetProvinceByCode(string provinceCode)
    {
    //   var ampher = await ctx.MAmphor.Where(x => x.ProvinceCode == provinceCode).ToListAsync();
      var province = await ctx.MProvince
      .Where(x => x.ProvinceCode == provinceCode).SingleOrDefaultAsync();
  
      return Ok(province);
    }
  }
}