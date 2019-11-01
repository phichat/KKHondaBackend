using KKHondaBackend.Data;
using KKHondaBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace KKHondaBackend.Controllers.Master
{
  [Produces("application/json")]
  [Route("api/Master/[controller]")]
  public class BankController : Controller
  {
    private readonly dbwebContext ctx;
    private readonly IBankingService iBankingService;

    public BankController(
        dbwebContext _ctx,
        IBankingService ibankingService
    )
    {
      ctx = _ctx;
      iBankingService = ibankingService;
    }

    [HttpGet("DropDown")]
    public IActionResult DropDown()
    {
      return Ok(iBankingService.GetDropdowns());
    }

    [HttpGet("GetBookBank")]
    public IActionResult GetBookBank()
    {
      return Ok(iBankingService.GetBankingAndDetail());
    }

    [HttpGet("GetBookBankByBankCode")]
    public IActionResult GetBookBankByBankCode(string bankCode)
    {
      if (bankCode == null)
        return NotFound();

      var result = iBankingService
      .GetBankingAndDetail()
      .Where(o => o.BankCode == bankCode)
      .FirstOrDefault();

      return Ok(result);
    }
  }
}