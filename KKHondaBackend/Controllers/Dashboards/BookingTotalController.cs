using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace KKHondaBackend.Controllers.Dashboards
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/Dashboards/BookingTotal")]
    public class BookingTotalController : Controller
    {
        private readonly dbwebContext ctx;

        public BookingTotalController(dbwebContext context)
        {
            ctx = context;
        }

        // GET: api/BookingTotal
        [HttpGet("GetByCon")]
        public IActionResult GetByCon(DateTime startDate, DateTime endDate)
        {
            var payMentType = ctx.SpDashboardBookingPaymentType
                .FromSql($"EXEC sp_DashboardBookingPaymentType {startDate.ToString("yyyy-MM-dd")}, {endDate.ToString("yyyy-MM-dd")}")
                .ToList();

            var bookingType = ctx.SpDashboardBookingType
                .FromSql($"EXEC sp_DashboardBookingType {startDate.ToString("yyyy-MM-dd")}, {endDate.ToString("yyyy-MM-dd")}")
                .ToList();

            var bookingDetail = ctx.SpDashboardBookingDetail
                .FromSql($"EXEC sp_DashboardBookingDetail {startDate.ToString("yyyy-MM-dd")}, {endDate.ToString("yyyy-MM-dd")}")
                .ToList();

            var obj = new Dictionary<string, object>
                {
                    { "bookingPayMentType", payMentType },
                    { "bookingType", bookingType },
                    {"bookingDetail", bookingDetail }
                };
            return Ok(obj);
        }        
    }
}
