using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using KKHondaBackend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KKHondaBackend.Controllers.Selling
{
    [Route("api/Selling/[controller]")]
    public class BookingController : Controller
    {
        private readonly dbwebContext ctx;
        private readonly IBookingServices iBooking;

        public BookingController(dbwebContext context, IBookingServices iBookingService)
        {
            ctx = context;
            iBooking = iBookingService;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(iBooking.GetBookingLists());

            } catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        // GET api/values/5
        [HttpGet("GetById")]
        public IActionResult Get(int bookingId)
        {
            try
            {
                return Ok(iBooking.GetBookingById(bookingId));

            } catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

    }
}


