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

        public IEnumerable<CarRegisRevListRes> RevList
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
                    CreateName = cre.Fullname,
                    CreateDate = rev.CreateDate,
                    UpdateBy = rev.UpdateBy,
                    UpdateName = upd.Fullname,
                    UpdateDate = rev.UpdateDate
                });
        }

        [HttpGet("All")]
        public IActionResult All()
        {
            return Ok(RevList.ToList());
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
                    value.TagRev.RevNo = iSysParamService.GenerateRegisRevNo(value.TagRev.BranchId);
                    value.TagRev.Status = RevStatus.Normal;
                    value.TagRev.CreateDate = DateTime.Now;
                    ctx.CarRegisRevList.Add(value.TagRev);
                    ctx.SaveChanges();

                    var sed = ctx.CarRegisSedList.First(x => x.SedNo == value.TagRev.SedNo);
                    sed.Status = SedStatus.Received;
                    ctx.Entry(sed).State = EntityState.Modified;
                    ctx.SaveChanges();

                    value.TagConList.ForEach(item =>
                    {
                        item.UpdateDate = DateTime.Now;
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
    }


}