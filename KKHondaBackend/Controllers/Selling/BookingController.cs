using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KKHondaBackend.Controllers.Selling
{
    [Route("api/Selling/[controller]")]
    public class BookingController : Controller
    {
        private readonly dbwebContext ctx;

        public BookingController(dbwebContext context)
        {
            ctx = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
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
            {
                val = "โอน";
            }
            else if (d == 3)
            {
                val = "เช็ค";
            }
            else if (d == 4)
            {
                val = "บัตรเคดิต";
            }
            return val;
        }

        private static string ValueOrEmpty(string str)
        {
            return str == null ? "" : str;
        }

        // GET api/values/5
        [HttpGet("GetById")]
        public IActionResult Get(int bookingId)
        {
            var items = (from item in ctx.BookingItem

                         join bra in ctx.ProductBrand on item.BrandId equals bra.BrandId into a1
                         from brand in a1.DefaultIfEmpty()

                         join cat in ctx.ProductCategory on item.CatId equals cat.CatId into a2
                         from category in a2.DefaultIfEmpty()

                         join cla in ctx.ProductClass on item.ClassId equals cla.ClassId into a3
                         from classes in a3.DefaultIfEmpty()

                         join col in ctx.ProductColor on item.ColorId equals col.ColorId into a4
                         from color in a4.DefaultIfEmpty()

                         join mod in ctx.ProductModel on item.ModelId equals mod.ModelId into a5
                         from model in a5.DefaultIfEmpty()

                         join typ in ctx.ProductType on item.TypeId equals typ.TypeId into a6
                         from type in a6.DefaultIfEmpty()

                         join uni in ctx.Sellunit on item.UnitId equals uni.UnitId into a7
                         from unit in a7.DefaultIfEmpty()

                         where item.BookingId.Equals(bookingId)
                         select new
                         {
                             bookingId = item.BookingId,
                             brandName = brand.BrandName,
                             catId = item.CatId,
                             catName = category.CatName,
                             className = classes.ClassName,
                             colorName = color.ColorName,
                             costNet = item.CostNet,
                             costPrice = item.CostPrice,
                             costVat = item.CostVat,
                             costVatPrice = item.CostVatPrice,
                             itemDetailType = item.ItemDetailType,
                             itemId = item.ItemId,
                             itemQty = item.ItemQty,
                             itemType = item.ItemType,
                             modelCode = model.ModelCode,
                             modelName = model.ModelName,
                             partCode = item.PartCode,
                             partClass = item.PartClass,
                             partName = item.PartName,
                             partSource = item.PartSource,
                             partSpareCode = item.PartSpareCode,
                             realDiscountB = item.RealDiscountB,
                             realDiscountP = item.RealDiscountP,
                             realVat = item.RealVat,
                             realNetPrice = item.RealNetPrice,
                             realVatPrice = item.RealVatPrice,
                             realSellPrice = item.RealSellPrice,
                             realTotalDiscount = item.RealTotalDiscount,
                             realDiscountPPrice = item.RealDiscountPPrice,
                             runId = item.RunId,
                             sellNet = item.SellNet,
                             sellVat = item.SellVat,
                             sellPrice = item.SellPrice,
                             sellVatPrice = item.SellVatPrice,
                             typeName = type.TypeName,
                             unitName = unit.UnitName
                         }).ToList();

            var booking = (from book in ctx.Booking
                           where book.BookingId.Equals(bookingId)
                           select new
                           {
                               address = book.BookAddress,
                               contractNo = book.BookContactNo,
                               deposit = book.BookDeposit,
                               distcountB = book.BookDiscountB,
                               distcountP = book.BookDiscountP,
                               distcountPPrice = book.BookDiscountPPrice,
                               email = book.BookEmail,
                               custCode = book.CustomerCode,
                               custFullName = ValueOrEmpty(book.BookTitleName) + " " + ValueOrEmpty(book.BookFName) + " " + ValueOrEmpty(book.BookSName),
                               genderCode = book.BookGender,
                               genderName = (book.BookGender == 1) ? "ชาย" : "หญิง",
                               idCard = book.BookIdCard,
                               bookingDate = book.BookingDate,
                               depositType = book.BookingDepositType,
                               bookingId = book.BookingId,
                               bookingNo = book.BookingNo,
                               ptymentType = book.BookingPaymentType,
                               bookingStatus = book.BookingStatus,
                               bookingType = book.BookingType,
                               netPrice = book.BookNetPrice,
                               nickName = book.BookNickName,
                               outStandingPrice = book.BookOutstandingPrice,
                               receiveDate = book.BookReceiveDate,
                               remark = book.BookRemark,
                               sellPrice = book.BookSellPrice,
                               totalDiscount = book.BookTotalDiscount,
                               vat = book.BookVat,
                               vatPrice = book.BookVatPrice,
                               freeAct = book.FreeAct,
                               freeTag = book.FreeTag,
                               freeWarranty = book.FreeWarranty,
                               bookingItem = items
                           });

            if (booking == null)
                return NotFound();

            return Ok(booking);

        }
    }
}


