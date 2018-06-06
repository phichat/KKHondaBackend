using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using KKHondaBackend.Data;

namespace KKHondaBackend.Services
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class BookingServices : IBookingServices
    {
        private readonly dbwebContext ctx;


        public BookingServices(dbwebContext context)
        {
            ctx = context;
        }

       
        public Booking GetBookingById(int id)
        {
            Booking booking = new Booking();
            BookingItem[] bookingItems = new BookingItem[] {};


             bookingItems = (from item in ctx.BookingItem

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

                         where item.BookingId == id
                         select new BookingItem
                         {
                             BookingId = item.BookingId,
                             BrandName = brand.BrandName,
                             CatId = item.CatId,
                             CatName = category.CatName,
                             ClassName = classes.ClassName,
                             ColorName = color.ColorName,
                             CostNet = item.CostNet,
                             CostPrice = item.CostPrice,
                             CostVat = item.CostVat,
                             CostVatPrice = item.CostVatPrice,
                             ItemDetailType = item.ItemDetailType,
                             ItemId = item.ItemId,
                             ItemQty = item.ItemQty,
                             ItemType = item.ItemType,
                             ModelCode = model.ModelCode,
                             ModelName = model.ModelName,
                             PartCode = item.PartCode,
                             PartClass = item.PartClass,
                             PartName = item.PartName,
                             PartSource = item.PartSource,
                             PartSpareCode = item.PartSpareCode,
                             RealDiscountB = item.RealDiscountB,
                             RealDiscountP = item.RealDiscountP,
                             RealVat = item.RealVat,
                             RealNetPrice = item.RealNetPrice,
                             RealVatPrice = item.RealVatPrice,
                             RealSellPrice = item.RealSellPrice,
                             RealTotalDiscount = item.RealTotalDiscount,
                             RealDiscountPPrice = item.RealDiscountPPrice,
                             RunId = item.RunId,
                             SellNet = item.SellNet,
                             SellVat = item.SellVat,
                             SellPrice = item.SellPrice,
                             SellVatPrice = item.SellVatPrice,
                             TypeName = type.TypeName,
                             UnitName = unit.UnitName
                         }).ToArray();



            booking = (from book in ctx.Booking
                           where book.BookingId == id
                           select new Booking
                           {
                               Address = book.BookAddress,
                               ContractNo = book.BookContactNo,
                               Deposit = book.BookDeposit,
                               DistcountB = book.BookDiscountB,
                               DistcountP = book.BookDiscountP,
                               DistcountPPrice = book.BookDiscountPPrice,
                               Email = book.BookEmail,
                               CustCode = book.CustomerCode,
                               CustFullName = ValueOrEmpty(book.BookTitleName) + " " + ValueOrEmpty(book.BookFName) + " " + ValueOrEmpty(book.BookSName),
                               GenderCode = book.BookGender,
                               GenderName = (book.BookGender == 1) ? "ชาย" : "หญิง",
                               IdCard = book.BookIdCard,
                               BookingDate = book.BookingDate,
                               DepositType = book.BookingDepositType,
                               BookingId = book.BookingId,
                               BookingNo = book.BookingNo,
                               PtymentType = book.BookingPaymentType,
                               BookingStatus = book.BookingStatus,
                               BookingType = book.BookingType,
                               NetPrice = book.BookNetPrice,
                               NickName = book.BookNickName,
                               OutStandingPrice = book.BookOutstandingPrice,
                               ReceiveDate = book.BookReceiveDate,
                               Remark = book.BookRemark,
                               SellPrice = book.BookSellPrice,
                               TotalDiscount = book.BookTotalDiscount,
                               Vat = book.BookVat,
                               VatPrice = book.BookVatPrice,
                               FreeAct = book.FreeAct,
                               FreeTag = book.FreeTag,
                               FreeWarranty = book.FreeWarranty,
                               BookingItem = bookingItems
                           }).SingleOrDefault();

            return booking;

        }

        public BookingList[] GetBookingLists()
        {
            BookingList[] bookingLists = new BookingList[] { };

            bookingLists = (from book in ctx.Booking

                        join u in ctx.User on book.CreateBy equals u.Id into a
                        from user in a.DefaultIfEmpty()

                        join item in ctx.BookingItem on book.BookingId equals item.BookingId into a1
                        from bookItem in a1.DefaultIfEmpty()

                        join mod in ctx.ProductModel on bookItem.ModelId equals mod.ModelId into a2
                        from model in a2.DefaultIfEmpty()

                        join col in ctx.ProductColor on bookItem.ColorId equals col.ColorId into a3
                        from color in a3.DefaultIfEmpty()

                        where book.BookingStatus.Equals(1) && bookItem.ItemDetailType.Equals(1)
                        select new BookingList
                        {
                            BookingId = book.BookingId,
                            BookingNo = book.BookingNo,
                            Status = "จอง",
                            PaymentType = book.BookingPaymentType == 1 ? "เงินสด" : "สินเชื่อ",
                            DepositType = CheckDepositType(book.BookingDepositType),
                            BookingDate = book.BookingDate,
                            ReceiveDate = book.BookReceiveDate,
                            CustFullName = ValueOrEmpty(book.BookTitleName) + " " + ValueOrEmpty(book.BookFName) + " " + ValueOrEmpty(book.BookSName),
                            IdCard = book.BookIdCard,
                            ContractNo = book.BookContactNo,
                            Email = book.BookEmail,
                            Address = book.BookAddress,
                            ModelCode = model.ModelCode,
                            ModelName = model.ModelName,
                            ColorName = color.ColorName,
                            NetPrice = book.BookNetPrice,
                            Deposit = book.BookDeposit,
                            OutStandingPrice = book.BookOutstandingPrice,
                            CreateDate = book.CreateDate,
                            CreateBy = user.Fullname
                        }).ToArray();

            return bookingLists;
        }

        public static string ValueOrEmpty(string str)
        {
            return str == null ? "" : str;
        }

        public static string CheckDepositType(int? d)
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
    }

}
