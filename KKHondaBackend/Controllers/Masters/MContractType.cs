using System.Linq;
using KKHondaBackend.Data;
using KKHondaBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace KKHondaBackend.Controllers.Master
{
  [ApiController]
  [Produces("application/json")]
  [Route("api/Master/[controller]")]
  public class MContractTypeController : Controller
  {
    private readonly IContractTypeService iType;
    public MContractTypeController(
        IContractTypeService _iType
    )
    {
      iType = _iType;
    }

    [HttpGet("Dropdown")]
    public IActionResult Dropdown()
    {
      return Ok(iType.GetDropdowns());
    }
  }
}