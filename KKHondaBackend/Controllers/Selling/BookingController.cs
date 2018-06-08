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
<<<<<<< HEAD
            var list = (from book in ctx.Booking

                        join u in ctx.User on book.CreateBy equals u.Id into a
                        from user in a.DefaultIfEmpty()

                        join item in ctx.BookingItem on book.BookingId equals item.BookingId into a1
                        from bookItem in a1.DefaultIfEmpty()

                        join mod in ctx.ProductModel on bookItem.ModelId equals mod.ModelId into a2
                        from model in a2.DefaultIfEmpty()

                        join col in ctx.ProductColor on bookItem.ColorId equals col.ColorId into a3
                        from color in a3.DefaultIfEmpty()

                        where book.BookingStatus == 1 && bookItem.ItemDetailType == 1
                        select new
                        {
                            bookingId = book.BookingId,
                            bookingNo = book.BookingNo,
                            status = "จอง",
                            paymentType = book.BookingPaymentType == 1 ? "เงินสด" : "สินเชื่อ",
                            depositType = CheckDepositType(book.BookingDepositType),
                            bookingDate = book.BookingDate,
                            receiveDate = book.BookReceiveDate,
                            custFullName = ValueOrEmpty(book.BookTitleName) + " " + ValueOrEmpty(book.BookFName) + " " + ValueOrEmpty(book.BookSName),
                            idCard = book.BookIdCard,
                            contractNo = book.BookContactNo,
                            email = book.BookEmail,
                            address = book.BookAddress,
                            modelCode = model.ModelCode,
                            modelName = model.ModelName,
                            colorName = color.ColorName,
                            netPrice = book.BookNetPrice,
                            deposit = book.BookDeposit,
                            outStandingPrice = book.BookOutstandingPrice,
                            createDate = book.CreateDate,
                            createBy = user.Fullname
                        });

            if (list == null)
                return NoContent();

            return Ok(list);
        }

        private static string CheckDepositType(int? d)
        {
            var val = "";

            if (d == 1)
            {
                val = "เงินสด";
            }
            else if (d == 2)
=======
            try
>>>>>>> 29deb8e200ce2c3d9b18557f4262c2c09a0a9ed9
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

<<<<<<< HEAD
            if (booking == null)
                return NotFound();
=======
            } catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
>>>>>>> 29deb8e200ce2c3d9b18557f4262c2c09a0a9ed9


        }

    }
}


