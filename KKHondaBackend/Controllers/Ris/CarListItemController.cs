using KKHondaBackend.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace KKHondaBackend.Controllers.Ris
{
    [Route("api/Ris/[controller]")]
    public class CarListItemController : Controller
    {
        private readonly dbwebContext ctx;

        public CarListItemController(dbwebContext _ctx)
        {
            ctx = _ctx;
        }

        [HttpGet("GetByBookingId")]
        public IActionResult GetByBookingId(int bookingId)
        {
            var list = ctx.CarRegisListItem.Where(x => x.BookingId == bookingId);
            return Ok(list.ToList());
        }
    }
}