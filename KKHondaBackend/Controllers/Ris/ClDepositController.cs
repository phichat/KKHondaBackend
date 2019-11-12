using KKHondaBackend.Data;
using KKHondaBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using KKHondaBackend.Entities;
using System;

namespace KKHondaBackend.Controllers.Ris
{
  [Produces("application/json")]
  [Route("api/Ris/[controller]")]
  public class ClDepositController : Controller
  {
    private readonly dbwebContext ctx;

    public ClDepositController(dbwebContext _ctx)
    {
      ctx = _ctx;
    }

    public IEnumerable<CarRegisClDepositRes> ClDepositList
    {
      get => (
        from clD in ctx.CarRegisClDeposit
        join brh in ctx.Branch on clD.BranchId equals brh.BranchId
        join _cr in ctx.User on clD.CreateBy equals _cr.Id into cr1
        join _up in ctx.User on clD.UpdateBy equals _up.Id into up1
        join _bkac in ctx.BankingAcc on clD.AccBankId equals _bkac.AccBankId into bkac1
        from bkac in bkac1.DefaultIfEmpty()
        join _bki in ctx.Bankings on bkac.AccBankCode equals _bki.BankCode into bki1
        from bki in bki1.DefaultIfEmpty()
        join _is in ctx.CompanyInsurance on clD.InsuranceCode equals _is.CompanyCode into is1
        join _ex in ctx.ExpensesOtherRis on clD.ExpenseTag equals _ex.ExpensesCode into ex1
        from cre in cr1.DefaultIfEmpty()
        from upd in up1.DefaultIfEmpty()
        from isu in is1.DefaultIfEmpty()
        from exp in ex1.DefaultIfEmpty()
        select new CarRegisClDepositRes
        {
          Id = clD.Id,
          ListBookingId = clD.ListBookingId,
          ExpenseTag = clD.ExpenseTag,
          ExpenseTagName = exp == null ? null : exp.ExpensesDescription,
          InsuranceCode = clD.InsuranceCode,
          InsuranceName = isu == null ? null : isu.CompanyName,
          ReceiptNo = clD.ReceiptNo,
          ReceiptDate = clD.ReceiptDate,
          TotalNetPrice1 = clD.TotalNetPrice1,
          TotalExpense = clD.TotalExpense,
          TotalPrice = clD.TotalPrice,
          PaymentType = clD.PaymentType,
          PaymentTypeDesc = PaymentType.Status.First(o => o.Id == clD.PaymentType).Desc,
          PaymentPrice = clD.PaymentPrice,
          Discount = clD.Discount,
          TotalPaymentPrice = clD.TotalPaymentPrice,
          PaymentDate = clD.PaymentDate,
          AccBankId = clD.AccBankId,
          BankName = bki == null ? null : bki.BankName,
          DocumentRef = clD.DocumentRef,
          Status = clD.Status,
          StatusDesc = ReceiveClStatus.Status.First(o => o.Id == clD.Status).Desc,
          BranchId = clD.BranchId,
          BranchName = brh == null ? null : brh.BranchName,
          CreateBy = clD.CreateBy,
          CreateName = cre == null ? null : cre.FullName,
          CreateDate = clD.CreateDate,
          UpdateBy = clD.UpdateBy,
          UpdateName = upd == null ? null : upd.FullName,
          UpdateDate = clD.UpdateDate,
          Reason = clD.Reason,
          Remark = clD.Remark
        }
      );
    }

    public IEnumerable<CarRegisListItemSummary> RegisListItem(string itemTag, List<int> listBookingId)
    {
      return (from d in ctx.CarRegisListItem
              join h in ctx.CarRegisList on (int)d.BookingId equals h.BookingId
              where d.ItemTag == itemTag && listBookingId.Contains((int)d.BookingId)
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
              });
    }

    [HttpGet("SearchClDepositList")]
    public IActionResult SearchClDepositList(SearchClDepositList value)
    {
      var list = ClDepositList.Where(x =>
        (value.Status != null && x.Status == value.Status) ||
        (!string.IsNullOrEmpty(value.ReceiptNo) && !string.IsNullOrEmpty(x.ReceiptNo) && x.ReceiptNo.IndexOf(value.ReceiptNo) > -1) ||
        (!string.IsNullOrEmpty(value.CreateName) && x.CreateName.IndexOf(value.CreateName) > -1) ||
        (value.ExpenseTag.Any() && value.ExpenseTag.Contains(x.ExpenseTag)) ||
        (value.PaymentType.Any() && value.PaymentType.Contains(x.PaymentType))
      );
      return Ok(list.ToList());
    }

    [HttpGet("All")]
    public IActionResult All()
    {
      return Ok(ClDepositList.ToList());
    }

    [HttpGet("GetDetailById")]
    public IActionResult GetById(int id)
    {
      var clDeposit = ctx.CarRegisClDeposit.Where(o => o.Id == id);
      var clSingle = clDeposit.Single();
      var listBookingId = clSingle.ListBookingId.Split(",").Select(Int32.Parse).ToList();
      var conList = RegisListItem(clSingle.ExpenseTag, listBookingId)
          .Select(o => new CarRegisClDepositDeposit
          {
            BookingId = o.BookingId,
            BookingNo = o.BookingNo,
            BookingDate = o.BookingDate,
            NetPrice1 = (decimal)o.ItemNetPrice1,
            Expense = (decimal)(o.ItemPrice2 + o.ItemPrice3),
            PaymentPrice = (decimal)o.PaymentPrice
          });

      var result = (
        from r in clDeposit
        join _cr in ctx.User on r.CreateBy equals _cr.Id into cr1
        join _bac in ctx.BankingAcc on r.AccBankId equals _bac.AccBankId into bac1
        from bac in bac1.DefaultIfEmpty()
        join _bk in ctx.Bankings on bac.AccBankCode equals _bk.BankCode into bk1
        from bki in bk1.DefaultIfEmpty()
        join _is in ctx.CompanyInsurance on r.InsuranceCode equals _is.CompanyCode into is1
        from cre in cr1.DefaultIfEmpty()
        from isu in is1.DefaultIfEmpty()
        where r.Id == id
        select new CarRegisClDepositDetail
        {
          ExpenseTag = r.ExpenseTag,
          InsuranceCode = r.InsuranceCode,
          InsuranceName = isu.CompanyName,
          PaymentType = r.PaymentType,
          PaymentPrice = r.PaymentPrice,
          Discount = r.Discount,
          TotalPaymentPrice = r.TotalPaymentPrice,
          PaymentDate = r.PaymentDate,
          AccBankId = r.AccBankId,
          BankName = bki.BankName,
          AccBankName = bac.AccBankName,
          AccBankNumber = bac.AccBankNumber,
          DocumentRef = r.DocumentRef,
          Status = r.Status,
          StatusDesc = ReceiveClStatus.Status.First(o => o.Id == r.Status).Desc,
          BranchId = r.BranchId,
          CreateBy = r.CreateBy,
          CreateName = cre.FullName,
          Reason = r.Reason,
          Remark = r.Remark,
          ConList = conList.ToList()
        }
      );
      return Ok(result.Single());
    }

    [HttpPost]
    public IActionResult Post([FromBody] CarRegisClDepositFormBody value)
    {
      using (var transaction = ctx.Database.BeginTransaction())
      {
        try
        {
          var bookingId = new List<int>();
          value.ConList.ForEach(item => bookingId.Add(item.BookingId));

          var carRegisListItem = ctx.CarRegisListItem
            .Where(x =>
              bookingId.Contains((int)x.BookingId) &&
              x.ItemTag == value.ExpenseTag).ToList();

          carRegisListItem.ForEach(item =>
          {
            item.PaymentPrice = item.ItemPriceTotal;
            item.PaymentStatus = PaymentStatus.IsPayment;
          });
          ctx.CarRegisListItem.UpdateRange(carRegisListItem);
          ctx.SaveChanges();

          var carHistory = ctx.CarHistory
            .Where(o => bookingId.Contains((int)o.BookingId))
            .OrderByDescending(o => o.CarId)
            .First();

          switch (value.ExpenseTag)
          {
            case ExpensesTag.EXP10003:
              var act = ctx.CompanyInsurance
                .Where(o => o.CompanyCode == value.InsuranceCode)
                .AsNoTracking()
                .Single();
              carHistory.PrbCompany = act == null ? null : act.CompanyName;
              break;

            case ExpensesTag.EXP10004:
              var war = ctx.CompanyInsurance
                .Where(o => o.CompanyCode == value.InsuranceCode)
                .AsNoTracking()
                .Single();
              carHistory.WarCompany = war == null ? null : war.CompanyName;
              break;
          }

          var listBookingId = string.Join(",", bookingId);
          var totalNetPrice1 = value.ConList.Sum(o => o.NetPrice1);
          var totalExpense = value.ConList.Sum(o => o.Expense);
          var totalPrice = totalNetPrice1 + totalExpense;

          var clDeposit = new CarRegisClDeposit
          {
            ListBookingId = listBookingId,
            InsuranceCode = value.InsuranceCode,
            TotalNetPrice1 = totalNetPrice1,
            TotalExpense = totalExpense,
            TotalPrice = totalPrice,
            PaymentType = value.PaymentType,
            PaymentPrice = value.PaymentPrice,
            Discount = value.Discount,
            TotalPaymentPrice = value.TotalPaymentPrice,
            PaymentDate = value.PaymentDate,
            ExpenseTag = value.ExpenseTag,
            AccBankId = value.AccBankId,
            DocumentRef = value.DocumentRef,
            Status = ReceiveClStatus.Normal,
            BranchId = value.BranchId,
            CreateBy = value.CreateBy,
            CreateDate = DateTime.Now
          };
          ctx.CarRegisClDeposit.Add(clDeposit);
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
    public IActionResult Cancel([FromBody] CarRegisClDepositCancel value)
    {
      using (var transaction = ctx.Database.BeginTransaction())
      {
        try
        {
          var item = ctx.CarRegisClDeposit.First(o => o.Id == value.Id);
          var listBookingId = item.ListBookingId.Split(',').Select(Int32.Parse).ToList();
          var regisListItem = ctx.CarRegisListItem
            .Where(o => listBookingId.Contains((int)o.BookingId) && o.ItemTag == item.ExpenseTag)
            .ToList();
          regisListItem.ForEach(o =>
          {
            o.PaymentPrice = 0;
            o.PaymentStatus = PaymentStatus.Cancel;
          });
          ctx.UpdateRange(regisListItem);
          ctx.SaveChanges();

          item.Status = ReceiveClStatus.Cancel;
          item.Reason = value.Reason;
          item.UpdateBy = value.UpdateBy;
          item.UpdateDate = DateTime.Now;
          ctx.Update(item);
          ctx.SaveChanges();

          transaction.Commit();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          transaction.Rollback();
          return StatusCode(500, ex.Message);
        }
        return NoContent();

      }
    }
  }
}
