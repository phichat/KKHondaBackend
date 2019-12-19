using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using KKHondaBackend.Services;

namespace KKHondaBackend.Controllers.Booking
{
  [ApiController]
  [Produces("application/json")]
  [Route("api/Booking")]
  public class BookingController : Controller
  {
    private readonly dbwebContext ctx;
    private readonly ISysParameterService iSysParamService;

    public BookingController(
        dbwebContext _ctx,
        ISysParameterService isysParamService
    )
    {
      ctx = _ctx;
      iSysParamService = isysParamService;
    }

    // GET: /<controller>/
    [HttpGet]
    public IActionResult Get()
    {
      var contract = (from db in ctx.CreditContract

                      join _branch in ctx.Branch on db.BranchId equals _branch.BranchId into a1
                      from branch in a1.DefaultIfEmpty()

                      join _status in ctx.MStatus on db.ContractStatus equals _status.Id into a5
                      from status in a5.DefaultIfEmpty()

                      join _contrachHire in ctx.MCustomer on db.ContractHire equals _contrachHire.CustomerCode into a7
                      from contrachHire in a7.DefaultIfEmpty()

                      join _sale in ctx.User on db.CreatedBy equals _sale.Id into a14
                      from sale in a14.DefaultIfEmpty()

                      select new
                      {
                        CalculateId = db.CalculateId,
                        BookingId = db.BookingId,
                        RefNo = db.RefNo,
                        ContractNo = db.ContractNo,
                        ContractDate = db.ContractDate,
                        //StatusDesc = status.StatusDesc,
                        //ContractHire = contrachHire.CustomerPrename + contrachHire.CustomerName + " " + contrachHire.CustomerSurname,
                        //SaleName = sale.FullName,
                        Remark = db.Remark
                      }).ToList();

      var obj = new Dictionary<string, object>
                {
                    {"contract", contract},
                    {"booking", "booking"},
                    {"contractItem", "contractItem"},
                    {"isPay", "isPay"},
                    {"isOutstanding", "isOutstanding"}
                };
      return Ok(obj);
    }

    [HttpGet("GetBookingCarDetail")]
    public IActionResult GetBookingCarDetail(int bookingId)
    {
      var list = (
        from booking in ctx.Booking
        join item in ctx.BookingItem on booking.BookingId equals item.BookingId
        join log in ctx.TransferLog on item.LogReceiveId equals log.LogId into _log
        join own in ctx.MCustomer on booking.CusSellCode equals own.CustomerCode into _own
        join mod in ctx.ProductModel on item.ModelId equals mod.ModelId into _mod
        join col in ctx.ProductColor on item.ColorId equals col.ColorId into _col
        join ban in ctx.ProductBrand on item.BrandId equals ban.BrandId into _ban
        join typ in ctx.ProductType on item.TypeId equals typ.TypeId into _typ

        from tfLog in _log.DefaultIfEmpty()
        from owner in _own.DefaultIfEmpty()
        from model in _mod.DefaultIfEmpty()
        from color in _col.DefaultIfEmpty()
        from brand in _ban.DefaultIfEmpty()
        from type in _typ.DefaultIfEmpty()

        where item.ItemType == 1 && item.ItemDetailType == 1 && booking.BookingId == bookingId
        select new BookingCarDetail
        {
          BookingId = booking.BookingId,
          OwnerCode = booking.CusSellCode,
          OwnerName = owner != null ? $"{owner.CustomerPrename}{owner.CustomerName} {owner.CustomerSurname}" : "",
          TypeId = item.TypeId,
          TypeName = type.TypeName,
          BrandId = item.BrandId,
          BrandName = brand.BrandName,
          ModelId = item.ModelId,
          ModelName = model.ModelName,
          ColorId = item.ColorId,
          ColorName = color.ColorName,
          EngineNo = tfLog.EngineNo,
          FrameNo = tfLog.FrameNo,
          LogReceiveId = tfLog.LogId
        }
      ).ToList();

      return Ok(list);
    }


    [HttpGet("GetBranchAutoComplete")]
    public IActionResult GetBranchAutoComplete()
    {
      var branch = (from db in ctx.Branch
                    select new
                    {
                      Value = db.BranchId,
                      Text = db.BranchCode + "-" + db.BranchName
                    }).ToList();


      return Ok(branch);
    }

    [HttpGet("GetProductTypeAutoComplete")]
    public IActionResult GetProductTypeAutoComplete()
    {
      var productBrand = (from db in ctx.ProductBrand
                          select new
                          {
                            Value = db.BrandId,
                            Text = db.BrandName
                          }).ToList();

      return Ok(productBrand);
    }

    [HttpGet("GetVersionAutoComplete")]
    public IActionResult GetVersionAutoComplete()
    {
      var productModel = (from db in ctx.ProductModel
                          select new
                          {
                            Value = db.ModelId,
                            Text = db.ModelName
                          }).ToList();


      return Ok(productModel);
    }

    [HttpGet("GetDesignAutoComplete")]
    public IActionResult GetDesignAutoComplete()
    {
      var productType = (from db in ctx.ProductType
                         select new
                         {
                           Value = db.TypeId,
                           Text = db.TypeName
                         }).ToList();

      return Ok(productType);
    }

    [HttpGet("GetColorAutoComplete")]
    public IActionResult GetColorAutoComplete()
    {
      var productColor = (from db in ctx.ProductColor
                          select new
                          {
                            Value = db.ColorId,
                            Text = db.ColorName
                          }).ToList();


      return Ok(productColor);
    }

    [HttpGet("GetBookingNameAutoComplete")]
    public IActionResult GetBookingNameAutoComplete()
    {
      var bookingName = (from db in ctx.Booking
                         select new
                         {
                           id = db.CustomerCode,
                           name = db.CustomerCode + "-" + db.BookTitleName + db.BookFName + " " + db.BookSName + "-" + db.BookIdCard
                         }).Where(x => x.id != null).Distinct().ToList();


      return Ok(bookingName);
    }

    [HttpGet("GetRegisNameAutoComplete")]
    public IActionResult GetRegisNameAutoComplete()
    {
      var regisName = (from db in ctx.Booking
                       select new
                       {
                         id = db.CusSellName + "-" + db.CusTaxNo,
                         name = db.CusSellName + "-" + db.CusTaxNo
                       }).Where(x => x.id != null).Distinct().ToList();


      return Ok(regisName);
    }

    [HttpGet("GetSellNameAutoComplete")]
    public IActionResult GetSellNameAutoComplete()
    {
      var sellName = (from db in ctx.User
                      select new
                      {
                        Value = db.Id,
                        Text = db.FullName
                      }).ToList();

      return Ok(sellName);
    }

    // [HttpGet("GetFinanceNameAutoComplete")]
    // public IActionResult GetFinanceNameAutoComplete()
    // {
    //   var FinanceName = (from db in ctx.FinanceList
    //                      select new
    //                      {
    //                        Value = db.FiId,
    //                        Text = db.FiName
    //                      }).ToList();

    //   return Ok(FinanceName);
    // }
  }
}
