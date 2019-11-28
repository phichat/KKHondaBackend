using KKHondaBackend.Data;
using KKHondaBackend.Models;
using KKHondaBackend.Services.Ris;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KKHondaBackend.Controllers.Ris
{
    // [ApiController]
    [Produces("application/json")]
    [Route("api/Ris/[controller]")]
    public class CarListItemController : Controller
    {
        private readonly dbwebContext ctx;
        private readonly IMSendbackService iMSendback;

        public CarListItemController(
            dbwebContext _ctx,
            IMSendbackService _iMSendback
            )
        {
            ctx = _ctx;
            iMSendback = _iMSendback;
        }

        [HttpGet("GetByBookingId")]
        public IActionResult GetByBookingId(int bookingId)
        {
            var list = ctx.CarRegisListItem.Where(x => x.BookingId == bookingId).AsNoTracking();
            
            return Ok(list.ToList());
        }
    }
}