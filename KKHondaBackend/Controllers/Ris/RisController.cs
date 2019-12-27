using KKHondaBackend.Data;
using KKHondaBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using KKHondaBackend.Entities;
using System;
using KKHondaBackend.Services;
using System.Threading.Tasks;

namespace KKHondaBackend.Controllers.Ris
{
  // [ApiController]
  [Produces("application/json")]
  [Route("api/[controller]")]
  public class RisController : Controller
  {
    private readonly dbwebContext ctx;
    private readonly ISysParameterService iSysParamService;
    private readonly ICustomerServices iCustomer;

    public RisController(dbwebContext _ctx,
        ISysParameterService isysParamService,
        ICustomerServices _iCustomer)
    {
      ctx = _ctx;
      iSysParamService = isysParamService;
      iCustomer = _iCustomer;
    }

    private async Task<IEnumerable<CarRegisListRes>> RegisList(List<string> tag)
    {
      var list = await (from crl in ctx.CarRegisList
                        join crli in RegisListItem(tag) on crl.BookingId equals crli.BookingId
                        join his in ctx.CarHistory on crl.BookingId equals his.BookingId
                        join brh in ctx.Branch on crl.BranchId equals brh.BranchId
                        join _cr in ctx.User on crl.CreateBy equals _cr.Id into cr1
                        join _up in ctx.User on crl.UpdateBy equals _up.Id into up1
                        from cre in cr1.DefaultIfEmpty()
                        from upd in up1.DefaultIfEmpty()

                        select new CarRegisListRes
                        {
                          BookingNo = crl.BookingNo,
                          RevNo = crl.RevNo,
                          Status1 = crl.Status1,
                          Status2 = crl.Status2,
                          BookingDate = crl.BookingDate,
                          BranchId = crl.BranchId,
                          BranchName = brh.BranchName,
                          BranchProvince = brh.BranchProvince,
                          CreateBy = crl.CreateBy,
                          CreateDate = crl.CreateDate,
                          CreateName = cre.FullName,
                          ENo = crl.ENo,
                          FNo = crl.FNo,
                          Price1 = crli.ItemPrice1,
                          Price2 = crli.ItemPrice2,
                          Price3 = crli.ItemPrice3,
                          VatPrice1 = crli.ItemVatPrice1,
                          NetPrice1 = crli.ItemNetPrice1,
                          CutBalance = crli.ItemCutBalance,
                          Province = his.Province,
                          Reason = crl.Reason,
                          Remark = crl.Remark,
                          Status1Desc = ConStatus1.Status.FirstOrDefault(x => x.Id == crl.Status1).Desc,
                          Status2Desc = ConStatus2.Status.FirstOrDefault(x => x.Id == crl.Status2).Desc,
                          State1 = crl.State1,
                          State2 = crl.State2,
                          TagNo = his.TagNo,
                          TagRegis = his.TagRegis,
                          TotalPrice = crl.TotalPrice,
                          TransportReceiptDate = crl.TransportReceiptDate,
                          TransportServiceCharge = crl.TransportServiceCharge,
                          UpdateBy = crl.UpdateBy,
                          UpdateDate = crl.UpdateDate,
                          UpdateName = upd.FullName,
                          BookingId = crl.BookingId,
                          OwnerCode = his.OwnerCode,
                          VisitorCode = his.VisitorCode,
                          PaymentType = crl.PaymentType,
                          PaymentPrice = crl.PaymentPrice,
                          DiscountPrice = crl.DiscountPrice,
                          TotalPaymentPrice = crl.TotalPaymentPrice,
                          AccBankId = crl.AccBankId,
                          PaymentDate = crl.PaymentDate,
                          DocumentRef = crl.DocumentRef
                        })
                  .OrderByDescending(x => x.BookingId)
                  .ToListAsync();
      return list;

    }

    private IEnumerable<CarRegisListItemSummary> RegisListItem(List<string> itemTag)
    {

      return (from d in ctx.CarRegisListItem
              join h in ctx.CarRegisList on (int)d.BookingId equals h.BookingId
              where itemTag.Contains(d.ItemTag) &&
                   h.Status1 != ConStatus1.Cancel &&
                   //  h.Status2 == null &&
                   d.PaymentStatus != PaymentStatus.IsPayment
              group d by new { h.BookingId, h.BookingNo, h.BookingDate } into g
              select new CarRegisListItemSummary
              {
                BookingId = g.Key.BookingId,
                BookingNo = g.Key.BookingNo,
                BookingDate = g.Key.BookingDate,
                ItemPrice1 = g.Sum(o => o.ItemPrice1 == null ? 0 : o.ItemPrice1),
                ItemPrice2 = g.Sum(o => o.ItemPrice2 == null ? 0 : o.ItemPrice2),
                ItemPrice3 = g.Sum(o => o.ItemPrice3 == null ? 0 : o.ItemPrice3),
                ItemVatPrice1 = g.Sum(o => o.ItemVatPrice1 == null ? 0 : o.ItemVatPrice1),
                ItemNetPrice1 = g.Sum(o => o.ItemNetPrice1 == null ? 0 : o.ItemNetPrice1),
                ItemCutBalance = g.Sum(o => o.ItemCutBalance == null ? 0 : o.ItemCutBalance),
                ItemPriceTotal = g.Sum(o => o.ItemPriceTotal == null ? 0 : o.ItemPriceTotal),
                PaymentPrice = g.Sum(o => o.PaymentPrice == null ? 0 : o.PaymentPrice)
              }).ToList();
    }

    [HttpGet("SearchRegisList")]
    public async Task<IActionResult> SearchRegisList([FromHeader] SearchRegisList value)
    {
      var tag = new List<string> {
        ExpensesTag.EXP10001,
        ExpensesTag.EXP10002,
        ExpensesTag.EXP10003,
        ExpensesTag.EXP10004
      };
      var list = (await RegisList(tag)).Where(item =>
        (value.Status1 != null && item.Status1 == value.Status1) ||
        (value.Status1 != null && item.Status2 == value.Status2) ||
        (!string.IsNullOrEmpty(value.BookingNo) && item.BookingNo.ToLower().Contains(value.BookingNo.ToLower())) ||
        (!string.IsNullOrEmpty(value.RevNo) && item.RevNo.ToLower().IndexOf(value.RevNo.ToLower()) > -1) ||
        (!string.IsNullOrEmpty(value.ENo) && item.ENo.ToLower().IndexOf(value.ENo.ToLower()) > -1) ||
        (!string.IsNullOrEmpty(value.FNo) && item.FNo.ToLower().IndexOf(value.FNo.ToLower()) > -1)
      );

      return Ok(list);
    }



    [HttpGet("SearchWaitingTag")]
    public IActionResult SearchWaitingTag(SearchWaitingTag value)
    {
      var carExcepts = ctx.CarRegisList
          .Where(x => x.Status1 != ConStatus1.Cancel)
          .Select(x => $"{x.ENo}{x.FNo}");

      var list = (from bk in ctx.Booking
                  join bi in ctx.BookingItem on bk.BookingId equals bi.BookingId
                  join sl in ctx.Sale on bk.BookingId equals sl.BookingId
                  join ct in ctx.CreditContract on bk.BookingId equals ct.BookingId
                  join own in ctx.MCustomer on ct.ContractOwner equals own.CustomerCode
                  join hir in ctx.MCustomer on ct.ContractHire equals hir.CustomerCode
                  join tl in ctx.TransferLog on bi.LogReceiveId equals tl.LogId
                  join u0 in ctx.User on bk.SellBy equals u0.Id into u1
                  from us in u1.DefaultIfEmpty()

                  // join f0 in ctx.FinanceList on bk.FiId equals f0.FiId into f1
                  // from fi in f1.DefaultIfEmpty()

                  where bi.LogReceiveId > 0 &&
                  bk.BookingStatus == BookingStatus.Sell &&
                  ct.ContractStatus != 0 &&
                  (!carExcepts.Contains($"{tl.EngineNo}{tl.FrameNo}"))

                  select new CarRegisWaitingTagRes
                  {
                    BookingPaymentType = bk.BookingPaymentType,
                    PaymentTypeDesc = BookingPaymentType.Status.FirstOrDefault(x => x.Id == bk.BookingPaymentType).Desc,
                    SellNo = sl.SellNo,
                    BookingNo = bk.BookingNo,
                    // CusSellName = bk.CusSellName,
                    // BookTitleName = bk.BookTitleName,
                    // BookFName = bk.BookFName,
                    // BookSName = bk.BookSName,
                    BookIdCard = bk.BookIdCard,
                    BookContactNo = bk.BookContactNo,
                    FreeAct = bk.FreeAct,
                    FreeTag = bk.FreeTag,
                    FreeWarranty = bk.FreeWarranty,
                    BookNetPrice = bk.BookNetPrice,
                    SellDate = bk.SellDate,
                    SellBy = bk.SellBy,
                    SellName = us.FullName,
                    OwnerCode = ct.ContractOwner,
                    OwnerFullName = $"{own.CustomerPrename}{own.CustomerName} {own.CustomerSurname}",
                    HireCode = ct.ContractHire,
                    HireIdCard = hir.IdCard,
                    HireFullName = $"{hir.CustomerPrename}{hir.CustomerName} {hir.CustomerSurname}",
                    // RegisName = ct.ContractOwner,
                    ENo = tl.EngineNo,
                    FNo = tl.FrameNo,
                    FiId = 0
                  }
      );

      list = list.Where(x =>
        (value.BookingPaymentType.Any() && value.BookingPaymentType.Contains(x.BookingPaymentType)) ||
        (!string.IsNullOrEmpty(value.SellNo) && x.SellNo.IndexOf(value.SellNo) > -1) ||
        (!string.IsNullOrEmpty(value.RegisName) && x.OwnerFullName.IndexOf(value.RegisName) > -1) ||
        (!string.IsNullOrEmpty(value.BookName) && x.HireFullName.IndexOf(value.BookName) > -1) ||
        (!string.IsNullOrEmpty(value.BookIdCard) && x.HireIdCard.IndexOf(value.BookIdCard) > -1) ||
        (!string.IsNullOrEmpty(value.ENo) && x.ENo.IndexOf(value.ENo) > -1) ||
        (!string.IsNullOrEmpty(value.FNo) && x.FNo.IndexOf(value.FNo) > -1)
      ).OrderBy(x => x.SellDate);

      return Ok(list.ToList());
    }

    [HttpGet("CarRegisReceive")]
    public async Task<IActionResult> CarRegisReceive()
    {
      var tag = new List<string> {
        ExpensesTag.EXP10001,
        ExpensesTag.EXP10002,
        ExpensesTag.EXP10003,
        ExpensesTag.EXP10004
      };
      var list = (await RegisList(tag)).Where(x => x.State1 != ConStatus1.Cancel && x.Status2 == null);
      return Ok(list.ToList());
    }

    [HttpGet("CarRegisReceiveTag")]
    public async Task<IActionResult> CarRegisReceiveTag()
    {
      var tag = new List<string> {
        ExpensesTag.EXP10001,
        ExpensesTag.EXP10002
      };
      var list = await (from crl in ctx.CarRegisList
                        join crli in RegisListItem(tag) on crl.BookingId equals crli.BookingId
                        join his in ctx.CarHistory on crl.BookingId equals his.BookingId
                        join brh in ctx.Branch on crl.BranchId equals brh.BranchId
                        join _cr in ctx.User on crl.CreateBy equals _cr.Id into cr1
                        join _up in ctx.User on crl.UpdateBy equals _up.Id into up1
                        from cre in cr1.DefaultIfEmpty()
                        from upd in up1.DefaultIfEmpty()

                        where crl.Status1 != ConStatus1.Cancel && crl.Status2 == null

                        select new CarRegisListRes
                        {
                          BookingNo = crl.BookingNo,
                          RevNo = crl.RevNo,
                          Status1 = crl.Status1,
                          Status2 = crl.Status2,
                          BookingDate = crl.BookingDate,
                          BranchId = crl.BranchId,
                          BranchName = brh.BranchName,
                          BranchProvince = brh.BranchProvince,
                          CreateBy = crl.CreateBy,
                          CreateDate = crl.CreateDate,
                          CreateName = cre.FullName,
                          ENo = crl.ENo,
                          FNo = crl.FNo,
                          Price1 = crli.ItemPrice1,
                          Price2 = crli.ItemPrice2,
                          Price3 = crli.ItemPrice3,
                          VatPrice1 = crli.ItemVatPrice1,
                          NetPrice1 = crli.ItemNetPrice1,
                          CutBalance = crli.ItemCutBalance,
                          Province = his.Province,
                          Reason = crl.Reason,
                          Remark = crl.Remark,
                          Status1Desc = ConStatus1.Status.FirstOrDefault(x => x.Id == crl.Status1).Desc,
                          Status2Desc = ConStatus2.Status.FirstOrDefault(x => x.Id == crl.Status2).Desc,
                          State1 = crl.State1,
                          State2 = crl.State2,
                          TagNo = his.TagNo,
                          TagRegis = his.TagRegis,
                          TotalPrice = crl.TotalPrice,
                          TransportReceiptDate = crl.TransportReceiptDate,
                          TransportServiceCharge = crl.TransportServiceCharge,
                          UpdateBy = crl.UpdateBy,
                          UpdateDate = crl.UpdateDate,
                          UpdateName = upd.FullName,
                          BookingId = crl.BookingId,
                          OwnerCode = his.OwnerCode,
                          VisitorCode = his.VisitorCode,
                          PaymentType = crl.PaymentType,
                          PaymentPrice = crl.PaymentPrice,
                          DiscountPrice = crl.DiscountPrice,
                          TotalPaymentPrice = crl.TotalPaymentPrice,
                          AccBankId = crl.AccBankId,
                          PaymentDate = crl.PaymentDate,
                          DocumentRef = crl.DocumentRef
                        })
                  .ToListAsync();
      return Ok(list);
    }


    private IEnumerable<CarRegisClDepositDeposit> RegisReceiveDeposits(string itemTag)
    {
      var tag = new List<string> { itemTag };
      return RegisListItem(tag)
        .Select(o => new CarRegisClDepositDeposit
        {
          BookingId = o.BookingId,
          BookingNo = o.BookingNo,
          BookingDate = o.BookingDate,
          NetPrice1 = (decimal)o.ItemNetPrice1,
          Expense = (decimal)(o.ItemPrice2 + o.ItemPrice3),
          PaymentPrice = (decimal)o.PaymentPrice
        }).ToList();

    }

    [HttpGet("CarRegisReceiveWaranty")]
    public IActionResult CarRegisReceiveWaranty()
    {
      return Ok(RegisReceiveDeposits(ExpensesTag.EXP10004));
    }

    [HttpGet("CarRegisReceiveAct")]
    public IActionResult CarRegisReceiveAct()
    {
      return Ok(RegisReceiveDeposits(ExpensesTag.EXP10003));
    }

    [HttpGet("GetByConNo")]
    public async Task<IActionResult> GetByConNo(string conNo)
    {
      var tag = new List<string> {
        ExpensesTag.EXP10001,
        ExpensesTag.EXP10002,
        ExpensesTag.EXP10003,
        ExpensesTag.EXP10004
      };
      var ris = (await RegisList(tag)).FirstOrDefault(x => x.BookingNo == conNo);
      if (ris.VisitorCode != null)
      {
        var v = await iCustomer.GetCustomerByCode(ris.VisitorCode);
        ris.VisitorName = $"{v.CustomerPrename}{v.CustomerName} {v.CustomerSurname}";
      };

      if (ris.OwnerCode != null)
      {
        var o = await iCustomer.GetCustomerByCode(ris.OwnerCode);
        ris.OwnerName = $"{o.CustomerPrename}{o.CustomerName} {o.CustomerSurname}";
      };
      return Ok(ris);
    }

    [HttpGet("GetByConNoListReceiveTag")]
    public async Task<IActionResult> GetByConNoListReceiveTag(List<string> conListNo)
    {
      // var value = conListNo.Split(new string[] { "," }, StringSplitOptions.None);
      var tag = new List<string> {
        ExpensesTag.EXP10001,
        ExpensesTag.EXP10002
      };
      var list = (await RegisList(tag)).Where(x => conListNo.Contains(x.BookingNo));
      return Ok(list.ToList());
    }

    [HttpGet("GetCarBySellNo")]
    public async Task<IActionResult> GetCarBySellNo(string sellNo)
    {

      var value = await (
         from booking in ctx.Booking
         join item in ctx.BookingItem on booking.BookingId equals item.BookingId

         join com in ctx.Company on booking.CusSellCode equals com.ComCode into _com
         from company in _com.DefaultIfEmpty()

         join log in ctx.TransferLog on item.LogReceiveId equals log.LogId into _log
         from tfLog in _log.DefaultIfEmpty()

         join mod in ctx.ProductModel on item.ModelId equals mod.ModelId into _mod
         from model in _mod.DefaultIfEmpty()

         join col in ctx.ProductColor on item.ColorId equals col.ColorId into _col
         from color in _col.DefaultIfEmpty()

         join ban in ctx.ProductBrand on item.BrandId equals ban.BrandId into _ban
         from brand in _ban.DefaultIfEmpty()

         join typ in ctx.ProductType on item.TypeId equals typ.TypeId into _typ
         from type in _typ.DefaultIfEmpty()

         where item.ItemDetailType == 1 &&
         item.LogReceiveId > 0 &&
         booking.SellNo == sellNo
         select new BookingCarDetail
         {
           BookingId = booking.BookingId,
           BookingPaymentType = (int)booking.BookingPaymentType,
           OwnerCode = booking.CusSellCode,
           OwnerName = booking.CusSellName,
           TypeId = item.TypeId,
           TypeName = type == null ? "" : type.TypeName,
           BrandId = item.BrandId,
           BrandName = brand == null ? "" : brand.BrandName,
           ModelId = item.ModelId,
           ModelName = model == null ? "" : model.ModelName,
           ColorId = item.ColorId,
           ColorName = color == null ? "" : color.ColorName,
           EngineNo = tfLog == null ? "" : tfLog.EngineNo,
           FrameNo = tfLog == null ? "" : tfLog.FrameNo,
           LogReceiveId = tfLog == null ? default(int) : tfLog.LogId,
           FreeAct = booking.FreeAct,
           FreeWarranty = booking.FreeWarranty,
           FreeTag = booking.FreeTag
         }
       ).SingleAsync();

      return Ok(value);
    }

    [HttpPost]
    public IActionResult Post([FromBody]CreateConFormBody value)
    {
      using (var transaction = ctx.Database.BeginTransaction())
      {
        try
        {
          var tagRegisList = value.TagRegis;
          tagRegisList.BookingNo = iSysParamService.GenerateConNo((int)tagRegisList.BranchId);
          tagRegisList.CreateDate = DateTime.Now;
          tagRegisList.Status1 = ConStatus1.Received; // ปกติ | รับเรื่อง
          ctx.Entry(tagRegisList).State = EntityState.Added;
          ctx.SaveChanges();

          var tagHistory = value.TagHistory;
          tagHistory.CarNo = iSysParamService.GenerateHistoryCarNo((int)tagHistory.BranchId);
          tagHistory.BookingId = tagRegisList.BookingId;
          ctx.Entry(tagHistory).State = EntityState.Added;
          ctx.SaveChanges();

          var tagListItem = value.TagListItem;
          tagListItem.ForEach(v =>
          {
            v.BookingId = tagRegisList.BookingId;
            ctx.Entry(v).State = EntityState.Added;
          });
          ctx.SaveChanges();

          transaction.Commit();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          transaction.Rollback();
          return StatusCode(500, ex.Message);
        }
      }
      return NoContent();
    }

    [HttpPost("Update")]
    public IActionResult Update([FromBody]UpdateConFormBody value)
    {
      using (var transaction = ctx.Database.BeginTransaction())
      {
        try
        {
          var tagRegisList = value.TagRegis;
          var con = ctx.CarRegisList
              .AsNoTracking()
              .Single(x => x.BookingId == tagRegisList.BookingId);

          var sed = ctx.CarRegisSedList
              .FirstOrDefault(x =>
                  x.Status != SedStatus.Cancel &&
                  x.ConList.Contains(con.BookingNo)
              );

          var newP3 = tagRegisList.Price3 > 0 ? tagRegisList.Price3 : 0;
          var oldP3 = con.Price3 > 0 ? con.Price3 : 0;

          // กรณีเบิกเงินค่าใช้จ่ายเพิ่ม หรือ แก้ไขเงินค่าใช้จ่าย 
          // ต้องไปอัพเดทค่าใช้จ่ายที่ ใบส่งเรื่องด้วย
          if (sed != null && newP3 > oldP3)
          {
            var price3 = newP3 - oldP3;
            sed.Price3 = (sed.Price3 > 0) ? sed.Price3 += price3 : price3;
            sed.Status = SedStatus.Normal;
            sed.Price2Remain += (decimal)price3;
            sed.TotalPrice += (decimal)price3;
            ctx.Entry(sed).State = EntityState.Modified;
          }
          else if (sed != null && newP3 < oldP3)
          {
            var price3 = oldP3 - newP3;
            sed.Price3 = (sed.Price3 > 0) ? sed.Price3 -= price3 : price3;
            sed.Price2Remain -= (decimal)price3;
            sed.TotalPrice -= (decimal)price3;
            ctx.Entry(sed).State = EntityState.Modified;
          }

          if (tagRegisList.Price2 > 0 && newP3 == 0)
          {
            tagRegisList.Status1 = ConStatus1.Withdraw1;
          }
          else if (newP3 > 0)
          {
            tagRegisList.Status1 = ConStatus1.Withdraw2;
          }

          tagRegisList.UpdateDate = DateTime.Now;
          ctx.Entry(tagRegisList).State = EntityState.Modified;
          ctx.SaveChanges();

          var tagHistory = value.TagHistory;
          ctx.Entry(tagHistory).State = EntityState.Modified;
          ctx.SaveChanges();

          var tagListItem = value.TagListItem;
          tagListItem.ForEach(v =>
          {
            if (v.RunId == 0)
            {
              v.BookingId = tagRegisList.BookingId;
              ctx.Entry(v).State = EntityState.Added;
            }
            else
            {
              ctx.Entry(v).State = EntityState.Modified;
            }
          });
          ctx.SaveChanges();

          var trashTagListItem = value.TrashTagListItem;
          trashTagListItem.ForEach(v =>
          {
            if (v.RunId > 0)
              ctx.Entry(v).State = EntityState.Deleted;
          });
          ctx.SaveChanges();

          transaction.Commit();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          transaction.Rollback();
          return StatusCode(500, ex.Message);
        }
      }
      return NoContent();
    }

    [HttpPost("Cancel")]
    public IActionResult Cancel([FromBody]CarRegisListCancel value)
    {
      try
      {
        var item = ctx.CarRegisList.FirstOrDefault(x => x.BookingNo == value.BookingNo);
        item.Reason = value.Reason;
        item.Status1 = ConStatus1.Cancel;
        item.UpdateDate = DateTime.Now;
        ctx.CarRegisList.Update(item);
        ctx.SaveChanges();
      }
      catch (DbUpdateConcurrencyException ex)
      {
        return StatusCode(500, ex.Message);
      }
      return NoContent();
    }
  }
}