using System;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using KKHondaBackend.Services;
using KKHondaBackend.Entities;

namespace KKHondaBackend.Controllers.Ris
{
  [Produces("application/json")]
  [Route("api/Ris/Al")]
  public class AlController : Controller
  {
    private readonly dbwebContext ctx;
    private readonly ISysParameterService iSysParamService;

    public AlController(dbwebContext _ctx,
        ISysParameterService isysParamService)
    {
      ctx = _ctx;
      iSysParamService = isysParamService;
    }

    private IEnumerable<CarRegisClSummary> ClList
    {
      get => ctx.CarRegisClList
          .Where(x => x.Status == AlStatus.Normal)
          .GroupBy(x => new { x.AlNo })
          .Select(x => new CarRegisClSummary
          {
            AlNo = x.Key.AlNo,
            BalancePrice = x.Where(o => o.AlNo == x.Key.AlNo).OrderByDescending(o => o.ClId).FirstOrDefault().BalancePrice,
            TotalPaymentPrice = x.Sum(o => o.TotalPaymentPrice),
            NetPrice = x.Where(o => o.AlNo == x.Key.AlNo).OrderByDescending(o => o.ClId).FirstOrDefault().NetPrice
          }).AsNoTracking();
    }

    private IEnumerable<CarRegisAlListRes> AlListRes
    {
      get => (from al in ctx.CarRegisAlList
              join sd in ctx.CarRegisSedList on al.SedNo equals sd.SedNo

              join cl1 in ClList on al.AlNo equals cl1.AlNo into _cl
              from cl in _cl.DefaultIfEmpty()

              join brh in ctx.Branch on al.BranchId equals brh.BranchId
              join usr in ctx.User on al.CreateBy equals usr.Id
              join usrUpd in ctx.User on al.UpdateBy equals usrUpd.Id into _upd
              from upd in _upd.DefaultIfEmpty()
              join bro in ctx.User on sd.CreateBy equals bro.Id into _bro
              from brow in _bro.DefaultIfEmpty()
              join bkac in ctx.BankingAcc on al.AccBankId equals bkac.AccBankId into bkac1
              from bka in bkac1.DefaultIfEmpty()
              join bak in ctx.Bankings on bka.AccBankCode equals bak.BankCode into _bk
              from bk in _bk.DefaultIfEmpty()
              select new CarRegisAlListRes
              {
                AlId = al.AlId,
                AlNo = al.AlNo,
                SedNo = al.SedNo,
                BorrowerName = brow.FullName,
                BalancePrice = cl.NetPrice > 0 ? cl.NetPrice - cl.TotalPaymentPrice : al.PaymentPrice,
                ReceivePrice = cl.NetPrice > 0 ? cl.TotalPaymentPrice : 0,
                NetPrice = cl.NetPrice > 0 ? cl.NetPrice : al.PaymentPrice,
                Price2Remain = al.Price2Remain,
                PaymentPrice = al.PaymentPrice,
                DiscountPrice = al.DiscountPrice,
                TotalPaymentPrice = al.TotalPaymentPrice,
                PaymentType = al.PaymentType,
                AccBankId = al.AccBankId,
                BankName = bk.BankName,
                AccBankName = bka.AccBankName,
                AccBankNumber = bka.AccBankNumber,
                PaymentDate = al.PaymentDate,
                DocumentRef = al.DocumentRef,
                Remark = al.Remark,
                Reason = al.Reason,
                Status = al.Status,
                StatusDesc = AlStatus.Status.FirstOrDefault(x => x.Id == al.Status).Desc,
                BranchId = al.BranchId,
                BranchName = brh.BranchName,
                CreateBy = al.CreateBy,
                CreateName = usr.FullName,
                CreateDate = al.CreateDate,
                UpdateBy = al.UpdateBy,
                UpdateName = upd.FullName,
                UpdateDate = al.UpdateDate
              }).AsNoTracking();
    }

    [HttpGet("NormalList")]
    public IActionResult NormalList() => Ok(AlListRes.Where(x => x.Status == AlStatus.Normal).ToList());

    // [HttpGet("SendingList")]
    // public IActionResult SendingList() => Ok(AlListRes.Where(x => x.Status == AlStatus.Sending).ToList());

    // [HttpGet("AllNotWaitingList")]
    // public IActionResult AllNotWaitingList() => Ok(AlListRes.Where(x => x.Status != AlStatus.Waiting).ToList());

    [HttpGet("All")]
    public IActionResult All() => Ok(AlListRes.ToList());

    [HttpGet("GetByAlNo")]
    public IActionResult GetByAlNo(string alNo) => Ok(AlListRes.FirstOrDefault(x => x.AlNo == alNo));

    [HttpGet("GetBySedNo")]
    public IActionResult GetBySedNo(string sedNo)
    {
      var res = AlListRes.Where(x => x.SedNo == sedNo && x.Status != AlStatus.Cancel).ToList();
      return Ok(res);
    }


    [HttpPost]
    public IActionResult Post([FromBody] CarRegisAlList value)
    {
      using (var transaction = ctx.Database.BeginTransaction())
      {
        try
        {
          value.AlNo = iSysParamService.GenerateAlNo(value.BranchId);
          value.UpdateDate = DateTime.Now;
          value.Status = AlStatus.Normal; // ปกติ
          ctx.Entry(value).State = EntityState.Added;
          ctx.SaveChanges();

          var sed = ctx.CarRegisSedList.FirstOrDefault(x => x.SedNo == value.SedNo);
          sed.Price2Remain = sed.Price2Remain - value.PaymentPrice;
          if (sed.Price2Remain == 0)
            sed.Status = SedStatus.Borrowed; // บันทึกยืมเงิน
          ctx.Entry(sed).State = EntityState.Modified;
          ctx.SaveChanges();

          var conList = sed.ConList.Split(',');
          foreach (string con in conList)
          {
            var ris = ctx.CarRegisList.FirstOrDefault(x => x.BookingNo == con);
            // กรณี Status1
            // Withdraw1 => อัพเดท Status2 = ส่งเรื่องดำเนินการครั้งที่ 1
            // Withdraw2 => อัพเดท Status2 = ส่งเรื่องดำเนินการครั้งที่ 2
            switch (ris.Status1)
            {
              case ConStatus1.Withdraw1:
                ris.Status2 = ConStatus2.Send1;
                break;

              case ConStatus1.Withdraw2:
                ris.Status2 = ConStatus2.Send2;
                break;
            }
            ctx.Entry(ris).State = EntityState.Modified;
          }
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

    [HttpPost("Cancel")]
    public IActionResult Cancel([FromBody]CarRegisAlCancel value)
    {
      try
      {
        var al = ctx.CarRegisAlList.FirstOrDefault(x => x.AlNo == value.AlNo);

        if (al == null) return BadRequest();

        al.Status = AlStatus.Cancel;
        al.Reason = value.Reason;
        al.Remark = value.Remark;
        al.UpdateBy = value.UpdateBy;
        al.UpdateDate = DateTime.Now;
        ctx.CarRegisAlList.Update(al);
        ctx.SaveChanges();

        var sed = ctx.CarRegisSedList.FirstOrDefault(x => x.SedNo == al.SedNo);
        sed.Price2Remain = sed.Price2Remain + al.PaymentPrice;
        sed.Status = SedStatus.Normal; // ปกติ
        ctx.Entry(sed).State = EntityState.Modified;
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