using KKHondaBackend.Data;
using KKHondaBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using KKHondaBackend.Entities;
using System;
using KKHondaBackend.Services;

namespace KKHondaBackend.Controllers.Ris
{
  [Produces("application/json")]
  [Route("api/Ris")]
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

    public IEnumerable<CarRegisListRes> RegisList(List<string> tag)
    {
      var list = (from crl in ctx.CarRegisList
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
                  }).OrderByDescending(x => x.BookingId).AsNoTracking();
      return list;
      ;
    }


    public IEnumerable<CarRegisListItemSummary> RegisListItem(List<string> itemTag)
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
    public IActionResult SearchRegisList(SearchRegisList value)
    {
      var tag = new List<string> {
        ExpensesTag.EXP10001,
        ExpensesTag.EXP10002,
        ExpensesTag.EXP10003,
        ExpensesTag.EXP10004
      };
      var list = RegisList(tag).Where(item =>
        (item.State1 == value.Status1 || item.Status2 == value.Status2) ||
        (!string.IsNullOrEmpty(value.BookingNo) && item.BookingNo.IndexOf(value.BookingNo) > -1) ||
        (!string.IsNullOrEmpty(value.RevNo) && item.RevNo.IndexOf(value.RevNo) > -1) ||
        (!string.IsNullOrEmpty(value.ENo) && item.ENo.IndexOf(value.ENo) > -1) ||
        (!string.IsNullOrEmpty(value.FNo) && item.FNo.IndexOf(value.FNo) > -1)
      );

      return Ok(list.ToList());
    }



    [HttpGet("SearchWaitingTag")]
    public IActionResult SearchWaitingTag(SearchWaitingTag value)
    {
      var carExcepts = ctx.CarRegisList
          .Where(x => x.Status1 != ConStatus1.Cancel)
          .Select(x => $"{x.ENo}{x.FNo}");

      var list = (from bk in ctx.Booking
                  join bi in ctx.BookingItem on bk.BookingId equals bi.BookingId
                  join tl in ctx.TransferLog on bi.LogReceiveId equals tl.LogId
                  join u0 in ctx.User on bk.SellBy equals u0.Id into u1
                  from us in u1.DefaultIfEmpty()

                  join f0 in ctx.FinanceList on bk.FiId equals f0.FiId into f1
                  from fi in f1.DefaultIfEmpty()

                  where bi.LogReceiveId > 0 &&
                  bk.BookingStatus == BookingStatus.Sell &&
                  (!carExcepts.Contains($"{tl.EngineNo}{tl.FrameNo}"))

                  select new CarRegisWaitingTagRes
                  {
                    BookingPaymentType = bk.BookingPaymentType,
                    PaymentTypeDesc = BookingPaymentType.Status.FirstOrDefault(x => x.Id == bk.BookingPaymentType).Desc,
                    SellNo = bk.SellNo,
                    BookingNo = bk.BookingNo,
                    CusSellName = bk.CusSellName,
                    BookTitleName = bk.BookTitleName,
                    BookFName = bk.BookFName,
                    BookSName = bk.BookSName,
                    BookIdCard = bk.BookIdCard,
                    BookContactNo = bk.BookContactNo,
                    FreeAct = bk.FreeAct,
                    FreeTag = bk.FreeTag,
                    FreeWarranty = bk.FreeWarranty,
                    BookNetPrice = bk.BookNetPrice,
                    SellDate = bk.SellDate,
                    SellBy = bk.SellBy,
                    SellName = us.FullName,
                    RegisName = bk.BookingPaymentType == BookingPaymentType.Leasing ? fi.FiName : bk.CusSellName,
                    ENo = tl.EngineNo,
                    FNo = tl.FrameNo,
                    FiId = bk.FiId
                  }
      );

      list = list.Where(x =>
        !string.IsNullOrEmpty(value.SellNo) && x.SellNo.IndexOf(value.SellNo) > -1 ||
        !string.IsNullOrEmpty(value.RegisName) && x.RegisName.IndexOf(value.RegisName) > -1 ||
        !string.IsNullOrEmpty(value.BookName) && ($"{x.BookTitleName}{x.BookFName}{x.BookSName}").IndexOf(value.BookName) > -1 ||
        !string.IsNullOrEmpty(value.BookIdCard) && x.BookIdCard.IndexOf(value.BookIdCard) > -1 ||
        !string.IsNullOrEmpty(value.ENo) && x.ENo.IndexOf(value.ENo) > -1 ||
        !string.IsNullOrEmpty(value.FNo) && x.FNo.IndexOf(value.FNo) > -1
      ).OrderBy(x => x.SellDate);

      if (value.BookingPaymentType != null)
      {
        list = list.Where(x => value.BookingPaymentType.Contains(x.BookingPaymentType));
      }
      // else
      // {
      //   var bookType = new List<int>{
      //     BookingPaymentType.Cash,
      //     BookingPaymentType.Leasing,
      //     BookingPaymentType.HierPurchase,
      //     BookingPaymentType.Credit
      //   };
      //   list = list.Where(x => bookType.Contains((int)x.BookingPaymentType));
      // };

      return Ok(list.ToList());
    }

    [HttpGet("CarRegisReceive")]
    public IActionResult CarRegisReceive()
    {
      var tag = new List<string> {
        ExpensesTag.EXP10001,
        ExpensesTag.EXP10002,
        ExpensesTag.EXP10003,
        ExpensesTag.EXP10004
      };
      var list = RegisList(tag).Where(x => x.State1 != ConStatus1.Cancel && x.Status2 == null);
      return Ok(list.ToList());
    }

    [HttpGet("CarRegisReceiveTag")]
    public IActionResult CarRegisReceiveTag()
    {
      var tag = new List<string> {
        ExpensesTag.EXP10001,
        ExpensesTag.EXP10002
      };
      var list = (from crl in ctx.CarRegisList
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
                  }).ToList();
      return Ok(list);
    }


    public IEnumerable<CarRegisClDepositDeposit> RegisReceiveDeposits(string itemTag)
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
      });
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
    public IActionResult GetByConNo(string conNo)
    {
      var tag = new List<string> {
        ExpensesTag.EXP10001,
        ExpensesTag.EXP10002,
        ExpensesTag.EXP10003,
        ExpensesTag.EXP10004
      };
      var ris = RegisList(tag).FirstOrDefault(x => x.BookingNo == conNo);
      if (ris.VisitorCode != null)
      {
        var v = iCustomer.GetCustomerByCode(ris.VisitorCode);
        ris.VisitorName = $"{v.CustomerPrename}{v.CustomerName} {v.CustomerSurname}";
      };

      if (ris.OwnerCode != null)
      {
        var o = iCustomer.GetCustomerByCode(ris.OwnerCode);
        ris.OwnerName = $"{o.CustomerPrename}{o.CustomerName} {o.CustomerSurname}";
      };
      return Ok(ris);
    }

    [HttpGet("GetByConNoListReceiveTag")]
    public IActionResult GetByConNoListReceiveTag(List<string> conListNo)
    {
      // var value = conListNo.Split(new string[] { "," }, StringSplitOptions.None);
      var tag = new List<string> {
        ExpensesTag.EXP10001,
        ExpensesTag.EXP10002
      };
      var list = RegisList(tag).Where(x => conListNo.Contains(x.BookingNo));
      return Ok(list.ToList());
    }

    [HttpGet("GetCarBySellNo")]
    public IActionResult GetCarBySellNo(string sellNo)
    {
      var value = (from bi in ctx.BookingItem
                   join bk in ctx.Booking on bi.BookingId equals bk.BookingId
                   join tl in ctx.TransferLog on bi.LogReceiveId equals tl.LogId
                   where bi.LogReceiveId > 0 && bk.SellNo == sellNo
                   select new
                   {
                     LogReceiveId = bi.LogReceiveId,
                     ENo = tl.EngineNo,
                     FNo = tl.FrameNo,
                     FreeAct = bk.FreeAct,
                     FreeTag = bk.FreeTag,
                     FreeWarranty = bk.FreeWarranty
                   }).FirstOrDefault();

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