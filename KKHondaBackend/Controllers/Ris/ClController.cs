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
                    join brh in ctx.Branch on cl.BranchId equals brh.BranchId
                    join usr in ctx.User on cl.CreateBy equals usr.Id
                    join usrUpd in ctx.User on cl.UpdateBy equals usrUpd.Id into _upd
                    from upd in _upd.DefaultIfEmpty()
                    join usRef in ctx.User on cl.RefundId equals usRef.Id into _usRf
                    from usRf in _usRf.DefaultIfEmpty()
                    join bak in ctx.Bankings on cl.BankCode equals bak.BankCode into _bk
                    from bk in _bk.DefaultIfEmpty()
                    select new CarRegisClListRes
                    {
                        ClId = cl.ClId,
                        AlNo = cl.AlNo,
                        ClNo = cl.ClNo,
                        BalancePrice = cl.BalancePrice,
                        ReceivePrice = cl.ReceivePrice,
                        NetPrice = cl.NetPrice,
                        PaymentType = cl.PaymentType,
                        PaymentDesc = PaymentType.Status.FirstOrDefault(x => x.Id == cl.PaymentType).Desc,
                        BankCode = cl.BankCode,
                        BankName = bk.BankName,
                        DocumentRef = cl.DocumentRef,
                        Remark = cl.Remark,
                        Status = cl.Status,
                        StatusDesc = ClStatus.Status.FirstOrDefault(x => x.Id == cl.Status).Desc,
                        RefundId = cl.RefundId,
                        RefundName = usRf.Fullname,
                        BranchId = cl.BranchId,
                        BranchName = brh.BranchName,
                        CreateBy = cl.CreateBy,
                        CreateName = usr.Fullname,
                        CreateDate = cl.CreateDate,
                        UpdateBy = cl.UpdateBy,
                        UpdateName = upd.Fullname,
                        UpdateDate = cl.UpdateDate
                    });
        }

        [HttpGet("All")]
        public IActionResult All() => Ok(ClListRes.ToList());

        [HttpPost]
        public IActionResult Post([FromBody]CarRegisClList value)
        {
            try
            {
                value.ClNo = iSysParamService.GenerateClNo(value.BranchId);
                value.RevNo = iSysParamService.GenerateRegisClRevNo(value.BranchId);
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
    }
}