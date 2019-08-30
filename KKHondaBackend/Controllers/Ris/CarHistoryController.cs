using System;
using KKHondaBackend.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace KKHondaBackend.Controllers.Ris
{
    [Route("api/Ris/[controller]")]
    public class CarHistoryController : Controller
    {
        private readonly dbwebContext ctx;
        public CarHistoryController(dbwebContext _ctx)
        {
            ctx = _ctx;
        }

        [HttpGet("GetByBookingId")]
        public IActionResult GetByBookingId(int bookingId)
        {
            var list = ctx.CarHistory.Where(x => x.BookingId == bookingId);
            return Ok(list.FirstOrDefault());
        }
    }
}