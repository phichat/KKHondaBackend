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
                        ctx.CarRegisListItemDoc.AddRange(value.TagListItemDoc);
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