using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using KKHondaBackend.Data;

namespace KKHondaBackend.Services
{
  public interface IBookingServices
  {
    Booking GetBookingById(int id);

    BookingList[] GetBookingLists();

  }

  public class Booking
  {
    public string Address { get; set; }
    public string ContractNo { get; set; }
    public decimal? Deposit { get; set; }
    public decimal? DistcountB { get; set; }
    public decimal? DistcountP { get; set; }
    public decimal? DistcountPPrice { get; set; }
    public string Email { get; set; }
    public string CustCode { get; set; }
    public string CustFullName { get; set; }
    public int? GenderCode { get; set; }
    public string GenderName { get; set; }
    public string IdCard { get; set; }
    public string BookingDate { get; set; }
    public int? BookingDepositType { get; set; }    // 1=เงินสด, 2=โอน, 3=เช็ค, 4=บัตรเครดิต, 5=เครดิตเทอม
    public int? BookingId { get; set; }
    public string BookingNo { get; set; }
    public int? BookingPaymentType { get; set; }    // ประเภทการซื้อ 1=สด, 2=สินเชื่อ, 3=เช่าซื้อ, 4=ขายเชื่อ
    public int? BookingStatus { get; set; }          // 1=จอง,2=ขาย,9=ยกเลิก
    public int? BookingType { get; set; }            // 1=รถ, 2=อื่นๆ, 3=ศูนย์บริการ
    public decimal? NetPrice { get; set; }
    public string NickName { get; set; }
    public decimal? OutStandingPrice { get; set; }
    public string ReceiveDate { get; set; }
    public string Remark { get; set; }
    public decimal? SellPrice { get; set; }
    public decimal? TotalDiscount { get; set; }
    public decimal? Vat { get; set; }
    public decimal? VatPrice { get; set; }
    public int? FreeAct { get; set; }
    public int? FreeTag { get; set; }
    public int? FreeWarranty { get; set; }
    public int? BranchId { get; set; }
    public int? CreateBy { get; set; }
    public BookingItem[] BookingItem { get; set; }

    public string CusSellName { get; set; }
    public string CusTaxNo { get; set; }
    public string CusTaxBranch { get; set; }
    public string SellRemark { get; set; }
  }

  public class BookingItem
  {
    public int? BookingId { get; set; }
    public string BrandName { get; set; }
    public int? CatId { get; set; }
    public string CatName { get; set; }
    public string ClassName { get; set; }
    public string ColorName { get; set; }
    public decimal? CostNet { get; set; }
    public decimal? CostPrice { get; set; }
    public decimal? CostVat { get; set; }
    public decimal? CostVatPrice { get; set; }
    public int? ItemDetailType { get; set; }
    public int? ItemId { get; set; }
    public decimal? ItemQty { get; set; }
    public decimal? ItemType { get; set; }
    public string ModelCode { get; set; }
    public string ModelName { get; set; }
    public string PartClass { get; set; }
    public string PartCode { get; set; }
    public string PartName { get; set; }
    public string PartSource { get; set; }
    public string PartSpareCode { get; set; }
    public decimal? RealDiscountB { get; set; }
    public decimal? RealDiscountP { get; set; }
    public decimal? RealVat { get; set; }
    public decimal? RealNetPrice { get; set; }
    public decimal? RealVatPrice { get; set; }
    public decimal? RealSellPrice { get; set; }
    public decimal? RealTotalDiscount { get; set; }
    public decimal? RealDiscountPPrice { get; set; }
    public int? RunId { get; set; }
    public decimal? SellNet { get; set; }
    public decimal? SellVat { get; set; }
    public decimal? SellPrice { get; set; }
    public decimal? SellVatPrice { get; set; }
    public string TypeName { get; set; }
    public string UnitName { get; set; }
    public string EngineNo { get; set; }
    public string FrameNo { get; set; }
  }

  public class BookingList
  {
    public int BookingId { get; set; }
    public string BookingNo { get; set; }
    public string Status { get; set; }
    public string PaymentType { get; set; }
    public string DepositType { get; set; }
    public string BookingDate { get; set; }
    public string ReceiveDate { get; set; }
    public string CustFullName { get; set; }
    public string IdCard { get; set; }
    public string ContractNo { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string ModelCode { get; set; }
    public string ModelName { get; set; }
    public string ColorName { get; set; }
    public decimal? NetPrice { get; set; }
    public decimal? Deposit { get; set; }
    public decimal? OutStandingPrice { get; set; }
    public DateTime? CreateDate { get; set; }
    public string CreateBy { get; set; }
  }

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
      BookingItem[] bookingItems = new BookingItem[] { };


      bookingItems = (from item in ctx.BookingItem
                      where item.BookingId == id

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

                      join _transferlog in ctx.TransferLog on item.LogReceiveId equals _transferlog.LogId into a8
                      from tflog in a8.DefaultIfEmpty()

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
                        UnitName = unit.UnitName,
                        EngineNo = tflog.EngineNo,
                        FrameNo = tflog.FrameNo
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
                   BookingDepositType = book.BookingDepositType,
                   BookingId = book.BookingId,
                   BookingNo = book.BookingNo,
                   BookingPaymentType = book.BookingPaymentType,
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
                   CreateBy = book.CreateBy,
                   BranchId = book.BranchId,
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
                        CreateBy = user.FullName
                      }).ToArray();

      return bookingLists;
    }

    public static string ValueOrEmpty(string str)
    {
      return str ?? "";
    }

    public static string CheckDepositType(int? d)
    {
      var val = "";
      switch (d)
      {
        case 1:
          val = "เงินสด";
          break;
        case 2:
          val = "โอน";
          break;
        case 3:
          val = "เช็ค";
          break;
        case 4:
          val = "บัตรเคดิต";
          break;
      }
      return val;
    }
  }

}
