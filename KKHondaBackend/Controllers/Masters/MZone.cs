using System.Linq;
using KKHondaBackend.Data;
using KKHondaBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace KKHondaBackend.Controllers.Master
{
  [ApiController]
  [Produces("application/json")]
  [Route("api/Master/[controller]")]
  public class MZoneController : Controller
  {
    private readonly IZoneService iZone;
    public MZoneController(
        IZoneService _iZone
    )
    {
      iZone = _iZone;
    }

    [HttpGet("Dropdown")]
    public IActionResult Dropdown()
    {
      return Ok(iZone.GetDropdowns());
    }
  }
}