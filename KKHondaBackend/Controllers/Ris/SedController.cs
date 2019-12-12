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
  [Route("api/Ris/Sed")]
  public class SedController : Controller
  {
    private readonly dbwebContext ctx;
    private readonly ISysParameterService iSysParamService;

    public SedController(dbwebContext _ctx,
        ISysParameterService isysParamService)
    {
      ctx = _ctx;
      iSysParamService = isysParamService;
    }

    private IEnumerable<CarRegisSedListRes> SedListRes
    {
      get => (from sed in ctx.CarRegisSedList
                // join al in ctx.CarRegisAlList on sed.SedNo equals al.SedNo into al1
                // from _al in al1.DefaultIfEmpty()
              join brh in ctx.Branch on sed.BranchId equals brh.BranchId
              join usr in ctx.User on sed.CreateBy equals usr.Id
              join usrUpd in ctx.User on sed.UpdateBy equals usrUpd.Id into _upd
              from upd in _upd.DefaultIfEmpty()
              select new CarRegisSedListRes
              {
                SedId = sed.SedId,
                SedNo = sed.SedNo,
                ConList = sed.ConList,
                // .Split(new string[] { "," }, StringSplitOptions.None),
                Price1 = sed.Price1,
                Price2 = sed.Price2,
                Price3 = sed.Price3,
                VatPrice1 = sed.VatPrice1,
                NetPrice1 = sed.NetPrice1,
                BorrowMoney = sed.BorrowMoney,
                // BorrowMoney = sed.Price2Remain == sed.Price2 ? sed.BorrowMoney : sed.BorrowMoney - sed.Price2Remain,
                Price2Remain = sed.Price2Remain,
                TotalPrice = sed.TotalPrice,
                Status = sed.Status,
                StatusDesc = SedStatus.Status.FirstOrDefault(x => x.Id == sed.Status).Desc,
                BranchId = sed.BranchId,
                BranchName = brh.BranchName,
                CreateBy = sed.CreateBy,
                CreateName = usr.FullName,
                CreateDate = sed.CreateDate,
                UpdateBy = sed.UpdateBy,
                UpdateName = upd.FullName,
                UpdateDate = sed.UpdateDate,
                Reason = sed.Reason,
                Remark = sed.Remark
              }).AsNoTracking();
    }

    [HttpGet("All")]
    public IActionResult All() => Ok(SedListRes.ToList());

    [HttpGet("SearchSedList")]
    public IActionResult SearchSedList(SearchSedList value)
    {
      var list = SedListRes.Where(x =>
          (!string.IsNullOrEmpty(value.SedNo) && x.SedNo.ToLower().IndexOf(value.SedNo.ToLower()) > -1) ||
          (!string.IsNullOrEmpty(value.CreateName) && x.CreateName.ToLower().IndexOf(value.CreateName.ToLower()) > -1) ||
          (value.CreateDate != null && x.CreateDate.Date == value.CreateDate?.Date) ||
          (value.Status != null && x.Status == value.Status)
      );
      return Ok(list.ToList());
    }

    [HttpGet("NormalList")]
    public IActionResult NormalList()
    {
      return Ok(SedListRes.Where(x => x.Status == SedStatus.Normal));
    }

    [HttpGet("BorrowedList")]
    public IActionResult BorrowedList()
    {
      return Ok(SedListRes.Where(x => x.Status == SedStatus.Borrowed));
    }

    [HttpGet("GetBySedNo")]
    public IActionResult GetBySedNo(string sedNo)
    {
      return Ok(SedListRes.FirstOrDefault(x => x.SedNo == sedNo));
    }

    [HttpGet("GetByTermSedNo")]
    public IActionResult GetByTermSedNo(string term)
    {
      var exclude = new List<int> { SedStatus.Cancel, SedStatus.Received };
      var list = SedListRes
      .Where(x => !exclude.Contains(x.Status) && x.SedNo.Contains(term.ToUpper()))
      .Take(100).ToList();
      return Ok(list);
    }

    [HttpPost]
    public IActionResult Post([FromBody]CarRegisSedList value)
    {
      try
      {
        value.SedNo = iSysParamService.GenerateSedNo(value.BranchId);
        value.CreateDate = DateTime.Now;
        value.Status = SedStatus.Normal; // ปกติ
        ctx.Entry(value).State = EntityState.Added;
        ctx.SaveChanges();

        var conList = value.ConList.Split(',');
        foreach (string con in conList)
        {
          var ris = ctx.CarRegisList.FirstOrDefault(x => x.BookingNo == con);
          ris.Status2 = ConStatus2.Send1;
          ctx.Entry(ris).State = EntityState.Modified;
        }
        ctx.SaveChanges();
      }
      catch (DbUpdateConcurrencyException ex)
      {
        return StatusCode(500, ex.Message);
      }
      return NoContent();
    }

    // [HttpPut("Cancel")]
    // public IActionResult Cancel(string sedNo){
    //     return Ok(sedNo);
    // }

    [HttpPost("Cancel")]
    public IActionResult Cancel([FromBody] CarRegisSedCancel c)
    {
      try
      {
        var value = ctx.CarRegisSedList.FirstOrDefault(x => x.SedNo == c.SedNo);
        value.Status = SedStatus.Cancel;
        value.UpdateDate = DateTime.Now;
        value.Reason = c.Reason;
        ctx.Entry(value).State = EntityState.Modified;
        ctx.SaveChanges();

        var conList = value.ConList.Split(',');
        foreach (string con in conList)
        {
          var ris = ctx.CarRegisList.FirstOrDefault(x => x.BookingNo == con);
          ris.Status2 = null;
          ctx.Entry(ris).State = EntityState.Modified;
        }
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