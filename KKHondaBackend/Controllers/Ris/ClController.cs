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
  // [ApiController]
  [Produces("application/json")]
  [Route("api/Ris/Cl")]
  public class ClController : Controller
  {
    private readonly dbwebContext ctx;
    private readonly ISysParameterService iSysParamService;

    public ClController(dbwebContext _ctx,
        ISysParameterService isysParamService)
    {
      ctx = _ctx;
      iSysParamService = isysParamService;
    }

    private IEnumerable<CarRegisClListRes> ClListRes
    {
      get => (from cl in ctx.CarRegisClList
              join al in ctx.CarRegisAlList on cl.AlNo equals al.AlNo
              join brh in ctx.Branch on cl.BranchId equals brh.BranchId
              join usr in ctx.User on cl.CreateBy equals usr.Id
              join usrUpd in ctx.User on cl.UpdateBy equals usrUpd.Id into _upd
              from upd in _upd.DefaultIfEmpty()
              join usRef in ctx.User on cl.RefundId equals usRef.Id into _usRf
              from usRf in _usRf.DefaultIfEmpty()
              join bak in ctx.BankingAcc on cl.AccBankId equals bak.AccBankId into _bk
              from bk in _bk.DefaultIfEmpty()
              select new CarRegisClListRes
              {
                ClId = cl.ClId,
                SedNo = al.AlNo,
                AlNo = cl.AlNo,
                ClNo = cl.ClNo,
                BalancePrice = cl.BalancePrice,
                PaymentPrice = cl.PaymentPrice,
                DiscountPrice = cl.DiscountPrice,
                TotalPaymentPrice = cl.TotalPaymentPrice,
                NetPrice = cl.NetPrice,
                PaymentDate = cl.PaymentDate,
                PaymentType = cl.PaymentType,
                PaymentDesc = PaymentType.Status.FirstOrDefault(x => x.Id == cl.PaymentType).Desc,
                AccBankId = cl.AccBankId,
                DocumentRef = cl.DocumentRef,
                Remark = cl.Remark,
                Reason = cl.Reason,
                Status = cl.Status,
                StatusDesc = ClStatus.Status.FirstOrDefault(x => x.Id == cl.Status).Desc,
                RefundId = cl.RefundId,
                RefundName = usRf.FullName,
                BranchId = cl.BranchId,
                BranchName = brh.BranchName,
                CreateBy = cl.CreateBy,
                CreateName = usr.FullName,
                CreateDate = cl.CreateDate,
                UpdateBy = cl.UpdateBy,
                UpdateName = upd.FullName,
                UpdateDate = cl.UpdateDate
              }).AsNoTracking();
    }

    [HttpGet("All")]
    public IActionResult All() => Ok(ClListRes.ToList());

    [HttpGet("SearchClList")]
    public IActionResult SearchClList(SearchClList value)
    {
      var list = ClListRes.Where(x =>
        (!string.IsNullOrEmpty(value.SedNo) && x.SedNo.IndexOf(value.SedNo) > -1) ||
        (!string.IsNullOrEmpty(value.AlNo) && x.AlNo.IndexOf(value.AlNo) > -1) ||
        (!string.IsNullOrEmpty(value.ClNo) && x.AlNo.IndexOf(value.ClNo) > -1) ||
        (!string.IsNullOrEmpty(value.CreateName) && x.CreateName.IndexOf(value.CreateName) > -1) ||
        (!string.IsNullOrEmpty(value.RefundName) && x.RefundName.IndexOf(value.RefundName) > -1) ||
        (value.CreateDate != null && x.CreateDate.Date == value.CreateDate?.Date) ||
        (value.Status != null && x.Status == value.Status)
      );
      return Ok(list.ToList());
    }

    [HttpGet("GetByClNo")]
    public IActionResult GetByClNo(string clNo)
    {
      return Ok(ClListRes.FirstOrDefault(x => x.ClNo == clNo));
    }

    [HttpPost]
    public IActionResult Post([FromBody]CarRegisClList value)
    {
      try
      {
        value.ClNo = iSysParamService.GenerateClNo(value.BranchId);
        value.CreateDate = DateTime.Now;
        value.Status = ClStatus.Normal;
        ctx.Entry(value).State = EntityState.Added;
        ctx.SaveChanges();

        if (value.BalancePrice == 0)
        {
          var sed = ctx.CarRegisAlList.FirstOrDefault(x => x.AlNo == value.AlNo);
          sed.Status = AlStatus.CashBack; // บันทึกคืนเงิน
          ctx.Entry(sed).State = EntityState.Modified;
          ctx.SaveChanges();
        }
      }
      catch (DbUpdateConcurrencyException ex)
      {
        return StatusCode(500, ex.Message);
      }
      return NoContent();
    }

    [HttpPost("Cancel")]
    public IActionResult Cancel([FromBody]CarRegisClCancel value)
    {
      try
      {
        var cl = ctx.CarRegisClList.FirstOrDefault(x => x.ClNo == value.ClNo);
        cl.UpdateDate = DateTime.Now;
        cl.UpdateBy = value.UpdateBy;
        cl.Status = ClStatus.Cancel;
        cl.Reason = value.Reason;
        cl.Remark = value.Remark;
        ctx.Entry(cl).State = EntityState.Modified;
        ctx.SaveChanges();

        var sed = ctx.CarRegisAlList.FirstOrDefault(x => x.AlNo == cl.AlNo);
        sed.Status = AlStatus.Normal; // ปกติ
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