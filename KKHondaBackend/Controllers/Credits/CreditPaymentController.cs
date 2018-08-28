using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;

namespace KKHondaBackend.Controllers.Credits
{
    [Produces("application/json")]
    [Route("api/Credit/Contract/CreditPayment")]
    public class CreditPaymentController : Controller
    {
        private readonly dbwebContext ctx;

        public CreditPaymentController(dbwebContext _ctx)
        {
            ctx = _ctx;
        }

        // GET: api/CreditPayment/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            try
            {
                var contract = (from db in ctx.CreditContract

                                join _branch in ctx.Branch on db.BranchId equals _branch.BranchId into a1
                                from branch in a1.DefaultIfEmpty()

                                join _status in ctx.MStatus on db.ContractStatus equals _status.Id into a5
                                from status in a5.DefaultIfEmpty()

                                join _contrachHire in ctx.MCustomer on db.ContractHire equals _contrachHire.CustomerCode into a7
                                from contrachHire in a7.DefaultIfEmpty()

                                join _sale in ctx.User on db.CreatedBy equals _sale.Id into a14
                                from sale in a14.DefaultIfEmpty()

                                where db.ContractId == id

                                select new
                                {
                                    ContractId = id,
                                    CalculateId = db.CalculateId,
                                    BookingId = db.BookingId,
                                    RefNo = db.RefNo,
                                    ContractNo = db.ContractNo,
                                    ContractDate = db.ContractDate,
                                    StatusDesc = status.StatusDesc,
                                    ContractHire = contrachHire.CustomerPrename + contrachHire.CustomerName + " " + contrachHire.CustomerSurname,
                                    SaleName = sale.Fullname,
                                    Remark = db.Remark
                                }).SingleOrDefault();

                var calculate = ctx.CreditCalculate.SingleOrDefault(p => p.CalculateId == contract.CalculateId);

                var booking = (from item in ctx.BookingItem

                               join bra in ctx.ProductBrand on item.BrandId equals bra.BrandId into a1
                               from brand in a1.DefaultIfEmpty()

                               join col in ctx.ProductColor on item.ColorId equals col.ColorId into a4
                               from color in a4.DefaultIfEmpty()

                               join mod in ctx.ProductModel on item.ModelId equals mod.ModelId into a5
                               from model in a5.DefaultIfEmpty()

                               join _transferlog in ctx.TransferLog on item.ItemId equals _transferlog.ItemId into a6
                               from transferlog in a6.DefaultIfEmpty()

                               where item.BookingId == id && item.ItemDetailType == 1
                               select new
                               {
                                   EngineNo = transferlog.EngineNo,
                                   FrameNo = transferlog.FrameNo,
                                   BrandName = brand.BrandName,
                                   ModelCode = model.ModelCode,
                                   Color = color.ColorName,
                                   Price = calculate.OutStandingPrice
                               }).SingleOrDefault();

                var isPay = ctx.CreditContractItem
                    .Where(x => x.ContractId == id && x.RefNo == contract.RefNo && x.PayNetPrice != null)
                    .GroupBy(x => x.RefNo, (key, values) => new
                    {
                        IsPayPrice = values.Sum(x => x.PayNetPrice),
                        IsPayTerm = values.Count()
                    }).SingleOrDefault();

                var isOutstanding = ctx.CreditContractItem
                    .Where(x => x.ContractId == id && x.RefNo == contract.RefNo && x.PayNetPrice == null)
                    .GroupBy(x => x.RefNo, (key, values) => new
                    {
                        IsOutstandingPrice = values.Sum(x => x.BalanceNetPrice),
                        IsOutstandingTerm = values.Count()
                    }).SingleOrDefault();

                var contractItem = (from db in ctx.CreditContractItem
                                    where db.ContractId == id && db.RefNo == contract.RefNo

                                    join _user in ctx.User on db.Payeer equals _user.Id into a1
                                    from user in a1.DefaultIfEmpty()

                                    select new
                                    {
                                        ContractItemId = db.ContractItemId,
                                        InstalmentNo = db.InstalmentNo,
                                        TaxInvoiceNo = db.TaxInvoiceNo,
                                        DueDate = db.DueDate,
                                        PayDate = db.PayDate,
                                        BalanceNetPrice = db.BalanceNetPrice,
                                        PayNetPrice = db.PayNetPrice,
                                        PaymentType = db.PaymentType,
                                        FineSum = db.FineSumStatus == 1 ? db.FineSum : 0,
                                        Remark = db.Remark,
                                        Payeer = user.Fullname
                                    }).ToList();

                var obj = new Dictionary<string, object>
                {
                    {"contract", contract},
                    {"booking", booking},
                    {"contractItem", contractItem},
                    {"isPay", isPay},
                    {"isOutstanding", isOutstanding}
                };

                return Ok(obj);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // POST: api/CreditPayment
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CreditPayment/5
        [HttpPost("PaymentTerm")]
        public async Task<IActionResult> PaymentTerm([FromBody] Payment[] payment)
        {
            try
            {
                var itemIds = payment.Select(p => p.ContractItemId).ToList();
                var contracts = ctx.CreditContractItem.Where(item => itemIds.Contains(item.ContractItemId)).ToList();
                foreach (var item in contracts)
                {
                    var _p = payment.SingleOrDefault(p => p.ContractItemId == item.ContractItemId);
                    item.PayDate = _p.PayDate;
                    item.PaymentType = _p.PaymentType;
                    item.Payeer = _p.Payeer;
                    item.PayPrice = item.Balance;
                    item.PayVatPrice = item.BalanceVatPrice;
                    item.PayNetPrice = item.BalanceNetPrice;
                    item.FineSumStatus = item.FineSumStatus == 0 ? 1 : item.FineSumStatus;
                    item.UpdateBy = _p.UpdateBy;
                    item.UpdateDate = DateTime.Now;
                }
                await ctx.SaveChangesAsync();
                
                var contractId = contracts.First().ContractId;
                var refNo = contracts.First().RefNo;
                
                var term = ctx.CreditContractItem
                    .Where(_item => _item.ContractId == contractId && _item.RefNo == refNo && _item.PayNetPrice == null)
                    .GroupBy(x => x.InstalmentNo, (key, values) => new
                    {
                        InstalmenNo = values.Count()
                    }).FirstOrDefault();

                if (term == null)
                {
                    // ถ้าชำระครบ จะเปลี่ยนสถานะเป็น ชำระครบรอโอนทะเบียน
                    var contract = ctx.CreditContract.FirstOrDefault(x => x.ContractId == contractId && x.RefNo == refNo);
                    contract.ContractStatus = 30;
                    await ctx.SaveChangesAsync();
                }

                return Get(contractId); ;
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpPost("CancelItemPayment")]
        public IActionResult CancelItemPayment([FromBody] CancelPayment cencel)
        {
            try
            {
                var item = ctx.CreditContractItem.SingleOrDefault(_item => _item.ContractItemId == cencel.ContractItemId);
                item.PayDate = null;
                item.PaymentType = null;
                item.PayPrice = null;
                item.PayVatPrice = null;
                item.PayNetPrice = null;
                item.Payeer = null;
                item.Remark = cencel.Remark;
                item.UpdateBy = cencel.UpdateBy;
                item.UpdateDate = DateTime.Now;
                ctx.SaveChanges();

                var ct = ctx.CreditContract.SingleOrDefault(x => x.ContractId == item.ContractId && x.RefNo == item.RefNo); 

                var term = ctx.CreditContractItem
                   .Where(_item => _item.ContractId == ct.ContractId && _item.RefNo == ct.RefNo && _item.PayNetPrice == null)
                   .GroupBy(x => x.InstalmentNo, (key, values) => new
                   {
                       InstalmenNo = values.Count()
                   }).FirstOrDefault();

                if (term.InstalmenNo > 0 && ct.ContractStatus == 30)
                {
                    // ถ้าชำครบระครบแล้วมีการยกเลิก จะเปลี่ยนสถานะเป็น อยู่ระหว่างผ่อนชำระ
                    ct.ContractStatus = 31;
                    ctx.SaveChanges();
                }

                return Get(item.ContractId);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        public class CancelPayment
        {
            public int ContractItemId { get; set; }
            public string Remark { get; set; }
            public int UpdateBy { get; set; }
        }

        public partial class Payment
        {
            public int ContractItemId { get; set; }
            public int ContractId { get; set; }
            public int Payeer { get; set; }
            public DateTime PayDate { get; set; }
            public int PaymentType { get; set; }
            public int UpdateBy { get; set; }
        }
    }
}
