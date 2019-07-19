using KKHondaBackend.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet("CarRegisList")]
        public IActionResult CarRegisList()
        {
            var list = (from crl in ctx.CarRegisList
                        join his in ctx.CarHistory on new { crl.ENo, crl.FNo } equals new { his.ENo, his.FNo }
                        join brh in ctx.Branch on crl.BookingId equals brh.BranchId
                        select new
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
                        }).ToList();
            return Ok(list);
        }
    }
}