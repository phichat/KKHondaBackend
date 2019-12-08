using System.Linq;
using KKHondaBackend.Data;
using KKHondaBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace KKHondaBackend.Controllers.Master
{
  [ApiController]
  [Produces("application/json")]
  [Route("api/Master/[controller]")]
  public class MContractGroupController : Controller
  {
    private readonly IContractGroupService iGroup;
    public MContractGroupController(
        IContractGroupService _iGroup
    )
    {
      iGroup = _iGroup;
    }

    [HttpGet("Dropdown")]
    public IActionResult Dropdown()
    {
      return Ok(iGroup.GetDropdowns());
    }
  }
}