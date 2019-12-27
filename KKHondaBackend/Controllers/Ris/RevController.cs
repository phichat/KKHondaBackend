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
  [Route("api/Ris/[controller]")]
  public class RevController : Controller
  {
    private readonly dbwebContext ctx;
    private readonly ISysParameterService iSysParamService;

    public RevController(dbwebContext _ctx,
    ISysParameterService isysParamService)
    {
      ctx = _ctx;
      iSysParamService = isysParamService;
    }

    private IEnumerable<CarRegisRevListRes> RevList
    {
      get => (
          from rev in ctx.CarRegisRevList
          join b in ctx.Branch on rev.BranchId equals b.BranchId into _b
          join c in ctx.User on rev.CreateBy equals c.Id into _c
          join u in ctx.User on rev.UpdateBy equals u.Id into _u
          from brh in _b.DefaultIfEmpty()
          from cre in _c.DefaultIfEmpty()
          from upd in _u.DefaultIfEmpty()
          select new CarRegisRevListRes
          {
            RevId = rev.RevId,
            RevNo = rev.RevNo,
            SedNo = rev.SedNo,
            BranchId = rev.BranchId,
            BranchName = brh.BranchName,
            TotalPrice1 = rev.TotalPrice1,
            TotalVatPrice1 = rev.TotalVatPrice1,
            TotalNetPrice = rev.TotalNetPrice,
            TotalCutBalance = rev.TotalCutBalance,
            TotalPrice2 = rev.TotalPrice2,
            TotalIncome = rev.TotalIncome,
            TotalClBalancePrice = rev.TotalClBalancePrice,
            TotalClReceivePrice = rev.TotalClReceivePrice,
            TotalExpenses = rev.TotalExpenses,
            TotalAccruedExpense = rev.TotalAccruedExpense,
            Remark = rev.Remark,
            Status = rev.Status,
            StatusDesc = RevStatus.Status.First(x => x.Id == rev.Status).Desc,
            CreateBy = rev.CreateBy,
            CreateName = cre.FullName,
            CreateDate = rev.CreateDate,
            UpdateBy = rev.UpdateBy,
            UpdateName = upd.FullName,
            UpdateDate = rev.UpdateDate,
            Reason = rev.Reason
          }).AsNoTracking();
    }

    [HttpGet("All")]
    public IActionResult All()
    {
      return Ok(RevList.ToList());
    }

    [HttpGet("SearchRevList")]
    public IActionResult SearchRevList(SearchRevList value)
    {
      var list = RevList.Where(x =>
          (!string.IsNullOrEmpty(value.RevNo) && x.RevNo.ToLower().IndexOf(value.RevNo.ToLower()) > -1) ||
          (!string.IsNullOrEmpty(value.CreateName) && x.CreateName.ToLower().IndexOf(value.CreateName.ToLower()) > -1) ||
          (value.CreateDate != null && x.CreateDate.Date == value.CreateDate?.Date) ||
          (value.Status != null && x.Status == value.Status)
      );
      return Ok(list.ToList());
    }

    [HttpGet("GetByRevNo")]
    public IActionResult GetByRevNo(string revNo)
    {
      var list = RevList.First(x => x.RevNo == revNo);
      return Ok(list);
    }

    [HttpPost]
    public IActionResult Post([FromBody]CarRegisRevListFormBody value)
    {
      using (var transaction = ctx.Database.BeginTransaction())
      {
        try
        {
          if (value.TagRev.RevId > 0)
          {
            value.TagRev.UpdateDate = DateTime.Now;
            ctx.Entry(value.TagRev).State = EntityState.Modified;
          }
          else
          {
            value.TagRev.RevNo = iSysParamService.GenerateRegisRevNo(value.TagRev.BranchId);
            value.TagRev.Status = RevStatus.Normal;
            value.TagRev.CreateDate = DateTime.Now;
            ctx.CarRegisRevList.Add(value.TagRev);
          }
          ctx.SaveChanges();

          var sed = ctx.CarRegisSedList.First(x => x.SedNo == value.TagRev.SedNo);
          sed.Status = SedStatus.Received;
          ctx.Entry(sed).State = EntityState.Modified;
          ctx.SaveChanges();

          value.TagConList.ForEach(item =>
          {
            item.UpdateDate = DateTime.Now;
            item.Status2 = ConStatus2.REV;
            item.RevNo = value.TagRev.RevNo;
            ctx.Entry(item).State = EntityState.Modified;
          });
          ctx.SaveChanges();

          ctx.CarRegisListItem.UpdateRange(value.TagConListItem);
          ctx.SaveChanges();

          if (value.TagListItemDoc.Any())
          {
            value.TagListItemDoc.ForEach(item =>
            {
              if (item.DocId == 0)
              {
                ctx.Entry(item).State = EntityState.Added;
              }
              else
              {
                ctx.Entry(item).State = EntityState.Modified;
              }
            });
            ctx.SaveChanges();
          }
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
    public IActionResult Cancel([FromBody]CarRegisRevList rev)
    {
      using (var transaction = ctx.Database.BeginTransaction())
      {
        try
        {
          rev.Status = 0;
          ctx.Entry(rev).State = EntityState.Modified;
          ctx.SaveChanges();

          var sed = ctx.CarRegisSedList.First(x => x.SedNo == rev.SedNo);
          var al = ctx.CarRegisAlList.FirstOrDefault(x => x.SedNo == rev.SedNo);

          sed.Status = al == null ? SedStatus.Normal : SedStatus.Borrowed;
          ctx.Entry(sed).State = EntityState.Modified;
          ctx.SaveChanges();

          var conList = sed.ConList.Split(',');
          foreach (string con in conList)
          {
            var ris = ctx.CarRegisList.FirstOrDefault(x => x.BookingNo == con);
            ris.CutBalance = ris.Price1;
            // ris.Status1
            // ris.Status2
            ris.State1 = null;
            ris.State2 = null;
            ctx.Entry(ris).State = EntityState.Modified;


            var ris_item = ctx.CarRegisListItem.Where(x => x.BookingId == ris.BookingId);
            foreach (var item in ris_item)
            {
              item.ItemCutBalance = item.ItemPrice1;
              item.State = null;
              item.DateReceipt = null;
              item.Remark = null;
              ctx.Entry(item).State = EntityState.Modified;
            }

            var doc_item = ctx.CarRegisListItemDoc.Where(x => x.BookingNo == con);
            foreach (var item in doc_item)
            {
              ctx.Entry(item).State = EntityState.Deleted;
            }
          }
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
  }


}