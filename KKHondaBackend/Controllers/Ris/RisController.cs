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
    [Route("api/Ris")]
    public class RisController : Controller
    {
        private readonly dbwebContext ctx;
        private readonly ISysParameterService iSysParamService;

        public RisController(dbwebContext _ctx,
            ISysParameterService isysParamService)
        {
            ctx = _ctx;
            iSysParamService = isysParamService;
        }

        public IEnumerable<CarRegisListRes> RegisList
        {
            get => (from crl in ctx.CarRegisList
                    join his in ctx.CarHistory on crl.BookingId equals his.BookingId
                    join brh in ctx.Branch on crl.BranchId equals brh.BranchId
                    join _cr in ctx.User on crl.CreateBy equals _cr.Id into cr1
                    join _up in ctx.User on crl.UpdateBy equals _up.Id into up1
                    from cre in cr1.DefaultIfEmpty()
                    from upd in up1.DefaultIfEmpty()
                    select new CarRegisListRes
                    {
                        BookingNo = crl.BookingNo,
                        Status1 = crl.Status1,
                        Status2 = crl.Status2,
                        BookingDate = crl.BookingDate,
                        BranchId = crl.BranchId,
                        BranchName = brh.BranchName,
                        BranchProvince = brh.BranchProvince,
                        CreateBy = crl.CreateBy,
                        CreateDate = crl.CreateDate,
                        CreateName = cre.FullName,
                        CutBalance = crl.CutBalance,
                        ENo = crl.ENo,
                        FNo = crl.FNo,
                        Price1 = crl.Price1,
                        Price2 = crl.Price2,
                        Price3 = crl.Price3,
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
                        VatPrice1 = crl.VatPrice1,
                        NetPrice1 = crl.NetPrice1,
                        BookingId = crl.BookingId
                    })
                    .Distinct()
                    .OrderByDescending(x => x.BookingId);
        }

        [HttpGet("All")]
        public IActionResult All() => Ok(RegisList.ToList());

        [HttpGet("GetAllByBranch")]
        public IActionResult GetAllByBranch(int branchId)
        {
            return Ok(RegisList.Where(x => x.BranchId == branchId).ToList());
        }

        [HttpGet("WaitingTag")]
        public IActionResult WaitingTag()
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
                        // (bk.FreeAct == 1 || bk.FreeTag == 1 || bk.FreeTag == 1) &&
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
            ).OrderBy(x => x.SellDate);
            return Ok(list.ToList());
        }

        // [HttpGet("CarRegisList")]
        // public IActionResult CarRegisList() => 
        //     Ok(RegisList.Where(x => x.BookingId != ConStatus.CompleteDelivery).ToList());

        // [HttpGet("CarRegisDeliver")]
        // public IActionResult CarRegisDeliver() => 
        //     Ok(RegisList.Where(x => x.BookingId == ConStatus.CompleteDelivery).ToList());

        [HttpGet("CarRegisReceive")]
        public IActionResult CarRegisReceive() =>
            Ok(RegisList.Where(x => x.State1 != ConStatus1.Cancel && x.Status2 == null).ToList());


        [HttpGet("GetByConNo")]
        public IActionResult GetByConNo(string conNo)
        {
            return Ok(RegisList.FirstOrDefault(x => x.BookingNo == conNo));
        }

        [HttpGet("GetByConNoList")]
        public IActionResult GetByConNoList(string conListNo)
        {
            var value = conListNo.Split(new string[] { "," }, StringSplitOptions.None);
            var a = RegisList.Where(x => value.Contains(x.BookingNo)).ToList();
            var b = RegisList.ToList();
            return Ok(a);
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
                        ctx.Entry(sed).State = EntityState.Modified;
                    }
                    else if (sed != null && newP3 < oldP3)
                    {
                        var price3 = oldP3 - newP3;
                        sed.Price3 = (sed.Price3 > 0) ? sed.Price3 -= price3 : price3;
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