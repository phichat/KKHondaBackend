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
    [Route("api/Ris")]
    public class RisController : Controller
    {
        private readonly dbwebContext ctx;

        public RisController(dbwebContext _ctx)
        {
            ctx = _ctx;
        }

        public IEnumerable<CarRegisListRes> RegisList
        {
            get => (from crl in ctx.CarRegisList
                    join his in ctx.CarHistory on new { crl.ENo, crl.FNo } equals new { his.ENo, his.FNo }
                    join brh in ctx.Branch on crl.BranchId equals brh.BranchId
                    select new CarRegisListRes
                    {
                        BookingId = crl.BookingId,
                        BookingNo = crl.BookingNo,
                        BookingStatus = crl.BookingStatus,
                        ENo = crl.ENo,
                        FNo = crl.FNo,
                        Price1 = crl.Price1,
                        Price2 = crl.Price2,
                        TotalPrice = crl.TotalPrice,
                        CreateBy = crl.CreateBy,
                        CreateDate = crl.CreateDate,
                        UpdateBy = crl.UpdateBy,
                        UpdateDate = crl.UpdateDate,
                        BranchName = brh.BranchName,
                        BranchProvince = brh.BranchProvince,
                        TransportReceiptDate = crl.TransportReceiptDate,
                        TransportServiceCharge = crl.TransportServiceCharge,
                        TagNo = his.TagNo
                    });
        }

        [HttpGet("CarRegisList")]
        public IActionResult CarRegisList() => Ok(RegisList.Where(x => x.BookingId != ConStatus.CompleteDelivery).ToList());

        [HttpGet("CarRegisDeliver")]
        public IActionResult CarRegisDeliver() => Ok(RegisList.Where(x => x.BookingId == ConStatus.CompleteDelivery).ToList());

        [HttpGet("CarRegisReceive")]
        public IActionResult CarRegisReceive() => Ok(RegisList.Where(x => x.BookingStatus == ConStatus.Received).ToList());

        [HttpGet("GetByConNoList")]
        public IActionResult GetByConNo(string conListNo)
        {
            var value = conListNo.Split(new string[] { "," }, StringSplitOptions.None);
            return Ok(RegisList.Where(x => value.Contains(x.BookingNo)).ToList());
        }

    }
}