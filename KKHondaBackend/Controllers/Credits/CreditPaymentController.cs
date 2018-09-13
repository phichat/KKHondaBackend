using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using KKHondaBackend.Services;

namespace KKHondaBackend.Controllers.Credits
{
    [Produces("application/json")]
    [Route("api/Credit/Contract/CreditPayment")]
    public class CreditPaymentController : Controller
    {
        private readonly dbwebContext ctx;
        private readonly ISysParameterService iSysParamService;

        public CreditPaymentController(
            dbwebContext _ctx,
            ISysParameterService isysParamService
        )
        {
            ctx = _ctx;
            iSysParamService = isysParamService;
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

                var _contractItem = ctx.CreditContractItem
                                   .Where(x => x.ContractId == id &&
                                   x.RefNo == contract.RefNo &&
                                   x.InitialPrice >= (decimal)0)
                                  .ToList();

                var deposit = _contractItem.SingleOrDefault(x => x.InstalmentNo == 0);

                var booking = (from item in ctx.BookingItem
                               where item.BookingId == contract.BookingId && item.ItemDetailType == 1

                               join bra in ctx.ProductBrand on item.BrandId equals bra.BrandId into a1
                               from brand in a1.DefaultIfEmpty()

                               join col in ctx.ProductColor on item.ColorId equals col.ColorId into a4
                               from color in a4.DefaultIfEmpty()

                               join mod in ctx.ProductModel on item.ModelId equals mod.ModelId into a5
                               from model in a5.DefaultIfEmpty()

                               join _transferlog in ctx.TransferLog on item.ItemId equals _transferlog.ItemId into a6
                               from transferlog in a6.DefaultIfEmpty()
                               where item.LogReceiveId == transferlog.LogId

                               select new
                               {
                                   EngineNo = transferlog.EngineNo,
                                   FrameNo = transferlog.FrameNo,
                                   BrandName = brand.BrandName,
                                   ModelCode = model.ModelCode,
                                   Color = color.ColorName,
                                   Price = calculate.Remain,
                                   DepositPrice = calculate.DepositPrice,
                                   DepositIsPay = deposit.PayDate != null ? deposit.PayNetPrice : 0,
                                   DepositIsOutstanding = deposit.PayDate == null ? deposit.PayNetPrice : 0,
                               }).SingleOrDefault();

                var isPay = _contractItem.Where(x => x.InstalmentNo > 0 && x.PayDate != null)
                               .GroupBy(o => new { o.ContractId })
                               .Select(g => new
                               {
                                   IsPayPrice = g.Sum(x => x.PayNetPrice),
                                   IsPayTerm = g.Count()
                               }).SingleOrDefault();


                var isOutstanding = _contractItem.Where(x => x.InstalmentNo > 0 && x.PayDate == null)
                                       .GroupBy(o => new { o.ContractId })
                                       .Select(g => new
                                       {
                                           IsOutstandingPrice = g.Sum(x => x.BalanceNetPrice),
                                           IsOutstandingTerm = g.Count()
                                       }).SingleOrDefault();

                var contractItem = (from db in _contractItem
                                    select new
                                    {
                                        ContractItemId = db.ContractItemId,
                                        TaxInvoiceNo = db.TaxInvoiceNo,
                                        InstalmentNo = db.InstalmentNo,
                                        DueDate = db.DueDate,
                                        PayDate = db.PayDate,
                                        BalanceNetPrice = db.BalanceNetPrice,
                                        PayNetPrice = db.PayDate == null ? 0 : db.PayNetPrice,
                                        PaymentType = db.PaymentType,
                                        FineSum = db.FineSumStatus == 1 ? db.FineSum : 0,
                                        Remark = db.Remark
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
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return StatusCode(500);
            }
        }


        // PUT: api/CreditPayment/5
        [HttpPost("PaymentTerm")]
        public async Task<IActionResult> PaymentTerm([FromBody] Payment payment)
        {
            using (var transaction = ctx.Database.BeginTransaction())
            {
                try
                {
                    var contract = ctx.CreditContract.SingleOrDefault(p => p.ContractId == payment.ContractId);
                    var calculate = ctx.CreditCalculate.SingleOrDefault(p => p.CalculateId == contract.CalculateId);
                    var contractItem = ctx.CreditContractItem.Where(p => p.ContractId == contract.ContractId && p.RefNo == contract.RefNo).ToList();

                    // เลือกข้อมูลการชำระเงินระหว่างวันที่กำหนดชำระ
                    var singContractItem = contractItem.SingleOrDefault(p => payment.DueDate == p.DueDate);

                    var payPriceExvat = payment.PayNetPrice / (1 + (singContractItem.VatRate / 100));

                    singContractItem.PayPrice = payPriceExvat;
                    singContractItem.PayVatPrice = payment.PayNetPrice - payPriceExvat;
                    singContractItem.PayNetPrice = payment.PayNetPrice;
                    singContractItem.Payeer = payment.Payeer;
                    singContractItem.PayDate = payment.PayDate;
                    singContractItem.PaymentType = payment.PaymentType;
                    singContractItem.UpdateBy = payment.UpdateBy;
                    singContractItem.UpdateDate = DateTime.Now;
                    singContractItem.TaxInvoiceBranchId = payment.BranchId;
                    singContractItem.TaxInvoiceNo = iSysParamService.GetnerateInstalmentTaxInvoiceNo(payment.BranchId);
                    singContractItem.ReceiptNo = iSysParamService.GetnerateReceiptNo(payment.BranchId);
                    singContractItem.Remark = payment.Remark;
                    singContractItem.DocumentRef = payment.DocumentRef;
                    singContractItem.DiscountPrice = payment.DisCountPrice;
                    singContractItem.DiscountRate = payment.DiscountRate;
                    // สถานะการใช้ส่วนลด
                    singContractItem.UseDiscount = payment.DisCountPrice > 0 ? 1 : 0;
                    // สถานะชำระครบ
                    singContractItem.Status = 11;

                    ctx.Update(singContractItem);
                   await ctx.SaveChangesAsync();


                    foreach (var item in contractItem)
                    {
                        if (item.InstalmentNo > 0)
                        {
                            var preItem = contractItem.SingleOrDefault(p => p.InstalmentNo == (item.InstalmentNo - 1));
                            // กำหนดเงินตั้งต้น
                            item.InitialPrice = (item.InstalmentNo == 1) ? item.InitialPrice : preItem.PrincipalRemain;
                            // หาดอกเบี้ยเงินต้น ค่างวด
                            item.InterestInstalment = (item.InitialPrice * calculate.Irr) / 100;
                            // หาเงินต้น ค่างวด
                            item.Principal = item.PayNetPrice - item.PayVatPrice - item.InterestInstalment;
                            // หาเงินต้นคงเหลือ
                            item.PrincipalRemain = item.InitialPrice - item.Principal;
                        }
                    }

                    if (singContractItem.InstalmentNo > 0)
                    {
                        var interest = contractItem.GroupBy(p => new { p.ContractId })
                                                   .Select(g => new
                                                   {
                                                       totalInterest = g.Sum(x => x.InterestInstalment)
                                                   }).SingleOrDefault();

                        foreach (var item in contractItem)
                        {
                            if (item.InstalmentNo > 0)
                            {
                                var preItem = contractItem.SingleOrDefault(p => p.InstalmentNo == (item.InstalmentNo - 1));
                                // ถ้าชำระงวดแรก รวมดอกเบี้ยค่างวด - ดอกเบี้ยงวดแรก
                                // ถ้างวดต่อไป ดอกเบี้ยคงเหลืองวดก่อน - ดอกเบี้ยค่างวด ปัจจุบัน
                                item.InterestPrincipalRemain = (item.InstalmentNo == 1)
                                    ? interest.totalInterest - item.InterestInstalment
                                    : preItem.InterestPrincipalRemain - item.InterestInstalment;

                                item.DiscountInterest = (decimal)item.InterestPrincipalRemain * (decimal)0.5;
                            }
                        }
                    }

                    ctx.UpdateRange(contractItem);
                   await ctx.SaveChangesAsync();

                    // เช็คว่า มีการชำระครบหรือยัง
                    var term = ctx.CreditContractItem
                                  .Where(p =>
                                         p.ContractId == contract.ContractId &&
                                         p.RefNo == contract.RefNo &&
                                         p.InstalmentNo > 0 &&
                                         p.PayDate != null)
                                   .GroupBy(o => new { o.ContractId })
                                  .Select(g => new
                                  {
                                      totalPaynetPrice = g.Sum(x => x.PayNetPrice)
                                  }).SingleOrDefault();
                    //.GroupBy(x => x.PayNetPrice, (key, values) => new
                    //{
                    //    totalPaynetPrice = values.Sum(x => x.PayNetPrice)
                    //}).FirstOrDefault();


                    if (term != null && term.totalPaynetPrice >= calculate.Remain)
                    {
                        // ถ้าชำระครบ จะเปลี่ยนสถานะเป็น ชำระครบรอโอนทะเบียน
                        contract.ContractStatus = 30;
                        contract.EndContractDate = DateTime.Now.Date;

                        ctx.Update(contract);
                       await ctx.SaveChangesAsync();
                    }

                    //await ctx.SaveChangesAsync();
                    transaction.Commit();

                    return Get(payment.ContractId);

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    transaction.Rollback();
                    return StatusCode(500);
                }
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
                    // ถ้าชำระครบ จะเปลี่ยนสถานะเป็น ชำระครบรอโอนทะเบียน
                    ct.ContractStatus = 30;
                    ct.EndContractDate = DateTime.Now;
                    ctx.SaveChanges();
                }

                return Get(item.ContractId);
            }
            catch (Exception)
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

        public class Payment
        {
            public int ContractId { get; set; }
            public decimal Outstanding { get; set; }
            public int PaymentType { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime PayDate { get; set; }
            public decimal PayNetPrice { get; set; }
            public int Payeer { get; set; }
            public decimal BalanceNetPrice { get; set; }
            public string Remark { get; set; }
            public int UpdateBy { get; set; }
            public int BranchId { get; set; }
            public string DocumentRef { get; set; }
            public decimal DisCountPrice { get; set; }
            public decimal DiscountRate { get; set; }
        }
    }
}
