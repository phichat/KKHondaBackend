using System;
using System.Collections.Generic;
using System.Linq;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using KKHondaBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace KKHondaBackend.Controllers.Reason
{
    [Produces("application/json")]
    [Route("api/Reason")]
    public class BookingCancelController : Controller
    {
        private readonly dbwebContext ctx;
        public BookingCancelController(
            dbwebContext _ctx,
            IBankingService ibankingService
        )
        {
            ctx = _ctx;
        }

        public IEnumerable<BookingReasonCode> list
        {
            get => ctx.BookingReasonCode.Where(x => x.Status == true);
        }

        [HttpGet("DropDown")]
        public IActionResult DropDown()
        {
            var reson = list.Select(x => new Dropdown
            {
                Value = x.CancelCode,
                Text = x.CancelDep
            }).ToList();
            return Ok(reson);
        }
    }
}