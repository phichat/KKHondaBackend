using System.Linq;
using KKHondaBackend.Data;
using KKHondaBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace KKHondaBackend.Controllers.Master
{
  [ApiController]
  [Produces("application/json")]
  [Route("api/Master/[controller]")]
  public class MStatusController : Controller
  {
    private readonly dbwebContext ctx;
    public MStatusController(
        dbwebContext _ctx
    )
    {
      ctx = _ctx;
    }

    [HttpGet("HPSDropdown")]
    public IActionResult HPSDropdown()
    {
      var dd = ctx.MStatus
      .Where(x => x.Id >= 16 && x.Id <= 33)
      .Select(x => new Dropdown
      {
        Value = x.Id.ToString(),
        Text = x.StatusDesc
      });
      return Ok(dd);
    }
  }
}