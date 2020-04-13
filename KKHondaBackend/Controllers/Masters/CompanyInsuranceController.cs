using System.Collections.Generic;
using KKHondaBackend.Data;
using KKHondaBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KKHondaBackend.Controllers.Master
{
    [Produces("application/json")]
    [Route("api/Master/[controller]")]
    public class CompanyInsuranceController : Controller
    {
        private readonly dbwebContext ctx;

        public CompanyInsuranceController(
            dbwebContext _ctx
        )
        {
            ctx = _ctx;
        }

        [HttpGet("DropDown")]
        public IActionResult DropDown()
        {
            var dd = ctx.CompanyInsurance.Select(x => new Dropdown
            {
                Value = x.CompanyCode,
                Text = x.CompanyName
            });
            return Ok(dd);
        }
    }
}