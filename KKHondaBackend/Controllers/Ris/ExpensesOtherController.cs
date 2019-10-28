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
    [Route("api/Ris/[controller]")]
    public class ExpensesOtherController : Controller
    {
        private readonly dbwebContext ctx;

        public ExpensesOtherController(dbwebContext _ctx)
        {
            ctx = _ctx;
        }

        private IEnumerable<ExpensesOtherRisRes> AllList
        {
            get => (from exp in ctx.ExpensesOtherRis
                    join _ty in ctx.ExpensesTypeOtherRis on exp.ExpensesType equals _ty.TypeId into _typ
                    join _cr in ctx.User on exp.CreateBy equals _cr.Id into _cr1
                    join _up in ctx.User on exp.UpdateBy equals _up.Id into _up1
                    from typ in _typ.DefaultIfEmpty()
                    from cre in _cr1.DefaultIfEmpty()
                    from upd in _up1.DefaultIfEmpty()
                    select new ExpensesOtherRisRes
                    {
                        ExpensesID = exp.ExpensesID,
                        ExpensesCode = exp.ExpensesCode,
                        ExpensesDescription = exp.ExpensesDescription,
                        ExpensesAmount = exp.ExpensesAmount,
                        ExpensesType = exp.ExpensesType,
                        ExpensesTypeDesc = typ.TypeName,
                        Status = exp.Status,
                        StatusDesc = ExpensesStatus.Status.FirstOrDefault(x => x.Id == (exp.Status ? 1 : 0)).Desc,
                        CreateBy = exp.CreateBy,
                        CreateName = cre.FullName,
                        DateCreate = exp.DateCreate,
                        UpdateBy = exp.UpdateBy,
                        UpdateName = upd.FullName,
                        DateUpdate = exp.DateUpdate
                    }).AsNoTracking();
        }

        private IEnumerable<ExpensesOtherRisRes> ActiveList
        {
            get => AllList.Where(x => x.Status == true);
        }

        [HttpGet]
        public IActionResult GetAction() => Ok(ActiveList.OrderBy(x => x.ExpensesType).ToList());

        [HttpGet("GroupType")]
        public IActionResult GroupType()
        {
            var group = ActiveList.Select(x => new
            {
                ExpensesType = x.ExpensesType,
                ExpensesTypeDesc = x.ExpensesTypeDesc
            })
            .Distinct()
            .OrderBy(x => x.ExpensesType);

            var newGroup = group.Select(x => new
            {
                ExpensesType = x.ExpensesType,
                ExpensesTypeDesc = x.ExpensesTypeDesc,
                ExpensesList = ActiveList
                    .Where(o => o.ExpensesType == x.ExpensesType)
                    .Select(o => new
                    {
                        ExpensesCode = o.ExpensesCode,
                        ExpensesDescription = o.ExpensesDescription
                    })
            });
            return Ok(newGroup.ToList());
        }
    }
}