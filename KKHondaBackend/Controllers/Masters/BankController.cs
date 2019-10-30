using KKHondaBackend.Data;
using KKHondaBackend.Services;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetBookBank() {
            return Ok(iBankingService.GetBankingAndDetail());
        }
    }
}