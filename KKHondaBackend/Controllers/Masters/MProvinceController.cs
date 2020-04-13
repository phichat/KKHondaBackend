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
    }
}