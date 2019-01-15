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
        private readonly IStatusService iStatusService;
        private readonly IBankingService iBankingService;

        public CreditPaymentController(
            dbwebContext _ctx,
            ISysParameterService isysParamService,
            IStatusService istatusService,
            IBankingService ibankingService
        )
        {
            ctx = _ctx;
            iSysParamService = isysParamService;
            iStatusService = istatusService;
            iBankingService = ibankingService;
        }

        // GET: api/CreditPayment/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            try
            {
                var bankingsDropdown = iBankingService.GetDropdowns();

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
                                    db.CalculateId,
                                    db.BookingId,
                                    db.RefNo,
                                    db.ContractNo,
                                    db.ContractDate,
                                    status.StatusDesc,
                                    ContractHire = contrachHire.CustomerPrename + contrachHire.CustomerName + " " + contrachHire.CustomerSurname,
                                    SaleName = sale.Fullname,
                                    db.Remark
                                }).FirstOrDefault();

                if (contract == null)
                    return StatusCode(404);


                var calculate = ctx.CreditCalculate.SingleOrDefault(p => p.CalculateId == contract.CalculateId);

                if (calculate == null)
                    return StatusCode(400);


                var _contractItem = ctx.CreditContractItem
                                   .Where(x => x.ContractId == id &&
                                   x.RefNo == contract.RefNo
                                   && x.InitialPrice >= (decimal)0
                                   ).ToList();

                if (_contractItem == null)
                    return StatusCode(400);


                _contractItem.ForEach(x =>
                {
                    var _FineSum = (decimal)CheckFineSum((DateTime)x.DueDate);
                    switch (x.FineSumStatus)
                    {
                        case null:
                        case 13: // ยังไม่ชำระ
                            x.FineSum = _FineSum;
                            x.FineSumRemain = _FineSum;
                            x.DelayDueDate = (int)(_FineSum / 100);
                            x.CheckDueDate = (DateTime.Now.Date);
                            break;

                        case 12:
                            var _CheckDueDate = (DateTime)x.CheckDueDate;
                            var _Compare = _CheckDueDate.Date.CompareTo(DateTime.Now.Date);

                            if (_Compare < 0) 
                            {
                                // ถ้า _Compare < 0 แสดงว่าต้องคิดค่าปรับของวันใหม่เพิ่มเข้าไป
                                // โดยที่ นำเอาวันที่ตรวจสอบล่าสุดนำไปเปรียบเที่ยบกับวันปัจจุบัน
                                _FineSum = (decimal)CheckFineSum(_CheckDueDate);
                                x.FineSum = x.FineSum + _FineSum;
                                x.FineSumRemain = x.FineSumRemain + _FineSum;
                                x.DelayDueDate = x.DelayDueDate + (int)(_FineSum / 100);
                                x.CheckDueDate = (DateTime.Now.Date);
                            }
                            break;
                    }
                });
                ctx.CreditContractItem.UpdateRange(_contractItem);
                ctx.SaveChanges();


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
                                   transferlog.EngineNo,
                                   transferlog.FrameNo,
                                   brand.BrandName,
                                   model.ModelCode,
                                   Color = color.ColorName,
                                   Price = calculate.Remain,
                                   calculate.DepositPrice,
                                   DepositIsPay = deposit.Status != 13 ? deposit.RemainNetPrice : 0,
                                   DepositIsOutstanding = deposit.Status == 13 ? deposit.RemainNetPrice : 0,
                               }).FirstOrDefault();

                var isPay = _contractItem.Where(x => x.InstalmentNo > 0 && x.Status != 13)
                               .GroupBy(o => new { o.ContractId })
                               .Select(g => new
                               {
                                   IsPayPrice = g.Sum(x => x.PayNetPrice),
                                   IsPayTerm = g.Count()
                               }).FirstOrDefault();


                var isOutstanding = _contractItem.Where(x => x.InstalmentNo > 0)
                                       .GroupBy(o => new { o.ContractId })
                                       .Select(g => new
                                       {
                                           IsOutstandingPrice = g.Sum(x => x.RemainNetPrice),
                                           IsOutstandingTerm = g.Count()
                                       }).FirstOrDefault();


                var contractItem = (from db in _contractItem

                                    join _s in ctx.MStatus on db.Status equals _s.Id into _ms
                                    from s in _ms.DefaultIfEmpty()

                                    select new
                                    {
                                        db.ContractItemId,
                                        db.TaxInvoiceNo,
                                        db.InstalmentNo,
                                        db.DueDate,
                                        db.PayDate,
                                        db.BalanceNetPrice,
                                        db.PayNetPrice,
                                        db.PaymentType,
                                        db.FineSum,
                                        db.FineSumRemain,
                                        db.FineSumOther,
                                        db.Remark,
                                        db.RemainNetPrice,
                                        db.Status,
                                        s.StatusDesc
                                    }).ToList();

                var obj = new Dictionary<string, object>
                {
                    {"bankingsDropdown", bankingsDropdown},
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

        private static double CheckFineSum(DateTime DueDate)
        {
            var Compare = DueDate.CompareTo(DateTime.Now.Date);
            var DelayDue = (DateTime.Now.Date - DueDate).TotalDays;
            return Compare < 0 ? DelayDue * 100 : 0;
        }


        // PUT: api/CreditPayment/5
        [HttpPost("PaymentTerm")]
        public async Task<IActionResult> PaymentTerm([FromBody] IPayment payment)
        {
            using (var transaction = ctx.Database.BeginTransaction())
            {
                try
                {
                    var contract = ctx.CreditContract.FirstOrDefault(p => p.ContractId == payment.ContractId);
                    var calculate = ctx.CreditCalculate.FirstOrDefault(p => p.CalculateId == contract.CalculateId);

                    var PayNetPrice = payment.PayNetPrice;

                    var _ContractItem = payment.CreditContractItem.ToList();
                    var _CreditContractItem = new List<CreditContractItem>();
                    _ContractItem.ForEach(x =>
                    {
                        var Item = ctx.CreditContractItem.SingleOrDefault(o => o.ContractItemId == x.ContractItemId);

                        var _PayNetPrice = PayNetPrice < Item.RemainNetPrice ? Item.RemainNetPrice - PayNetPrice : Item.RemainNetPrice;

                        var _RemainNetPrice = Item.RemainNetPrice - _PayNetPrice;

                        var payPriceExvat = _PayNetPrice / (1 + (Item.VatRate / 100));
                        var remainNetPriceExVat = _RemainNetPrice / (1 + (Item.VatRate / 100));

                        Item.PayPrice = payPriceExvat;
                        Item.PayVatPrice = _PayNetPrice - payPriceExvat;
                        Item.PayNetPrice = _PayNetPrice;

                        Item.Remain = remainNetPriceExVat;
                        Item.RemainVatPrice = _RemainNetPrice - remainNetPriceExVat;
                        Item.RemainNetPrice = _RemainNetPrice;

                        Item.Status = PayNetPrice < Item.RemainNetPrice ? 12 : 11;

                        Item.Payeer = payment.UpdateBy;
                        Item.PayDate = payment.PayDate;
                        Item.PaymentType = payment.PaymentType;

                        if (x.FineSum > 0)
                        {
                            Item.FineSumRemain = 0;
                            Item.FineSumStatus = 11;
                        }
                        
                        Item.FineSumOther = payment.FineSumOther;

                        Item.TaxInvoiceBranchId = payment.BranchId;
                        Item.TaxInvoiceNo = iSysParamService.GenerateInstalmentTaxInvoiceNo(payment.BranchId);
                        Item.ReceiptNo = iSysParamService.GenerateReceiptNo(payment.BranchId);
                        Item.Remark = payment.Remark;
                        Item.DocumentRef = payment.DocumentRef;

                         Item.UpdateBy = payment.UpdateBy;
                         Item.UpdateDate = DateTime.Now;

                        PayNetPrice -= Item.RemainNetPrice;

                        _CreditContractItem.Add(Item);
                    });

                    //var contractItem = ctx.CreditContractItem.Where(p => p.ContractId == contract.ContractId && p.RefNo == contract.RefNo).ToList();

                   // // เลือกข้อมูลการชำระเงินระหว่างวันที่กำหนดชำระ
                   // var singContractItem = contractItem.SingleOrDefault(p => payment.DueDate == p.DueDate);

                   // var payPriceExvat = payment.PayNetPrice / (1 + (singContractItem.VatRate / 100));

                   // singContractItem.PayPrice = payPriceExvat;
                   // singContractItem.PayVatPrice = payment.PayNetPrice - payPriceExvat;
                   // singContractItem.PayNetPrice = payment.PayNetPrice;
                   // singContractItem.Payeer = payment.Payeer;
                   // singContractItem.PayDate = payment.PayDate;
                   // singContractItem.PaymentType = payment.PaymentType;
                   // singContractItem.UpdateBy = payment.UpdateBy;
                   // singContractItem.UpdateDate = DateTime.Now;
                   // singContractItem.TaxInvoiceBranchId = payment.BranchId;
                   // singContractItem.TaxInvoiceNo = iSysParamService.GenerateInstalmentTaxInvoiceNo(payment.BranchId);
                   // singContractItem.ReceiptNo = iSysParamService.GenerateReceiptNo(payment.BranchId);
                   // singContractItem.Remark = payment.Remark;
                   // singContractItem.DocumentRef = payment.DocumentRef;
                   // singContractItem.DiscountPrice = payment.DisCountPrice;
                   // singContractItem.DiscountRate = payment.DiscountRate;
                   // // สถานะการใช้ส่วนลด
                   // singContractItem.UseDiscount = payment.DisCountPrice > 0 ? 1 : 0;
                   // // สถานะชำระครบ
                   // singContractItem.Status = 11;

                   // ctx.Update(singContractItem);
                   ////await ctx.SaveChangesAsync();


                   // foreach (var item in contractItem)
                   // {
                   //     if (item.InstalmentNo > 0)
                   //     {
                   //         var preItem = contractItem.SingleOrDefault(p => p.InstalmentNo == (item.InstalmentNo - 1));
                   //         // กำหนดเงินตั้งต้น
                   //         item.InitialPrice = (item.InstalmentNo == 1) ? item.InitialPrice : preItem.PrincipalRemain;
                   //         // หาดอกเบี้ยเงินต้น ค่างวด
                   //         item.InterestInstalment = (item.InitialPrice * calculate.Irr) / 100;
                   //         // หาเงินต้น ค่างวด
                   //         item.Principal = item.PayNetPrice - item.PayVatPrice - item.InterestInstalment;
                   //         // หาเงินต้นคงเหลือ
                   //         item.PrincipalRemain = item.InitialPrice - item.Principal;
                   //     }
                   // }

                   // if (singContractItem.InstalmentNo > 0)
                   // {
                   //     var interest = contractItem.GroupBy(p => new { p.ContractId })
                   //                                .Select(g => new
                   //                                {
                   //                                    totalInterest = g.Sum(x => x.InterestInstalment)
                   //                                }).SingleOrDefault();

                   //     foreach (var item in contractItem)
                   //     {
                   //         if (item.InstalmentNo > 0)
                   //         {
                   //             var preItem = contractItem.SingleOrDefault(p => p.InstalmentNo == (item.InstalmentNo - 1));
                   //             // ถ้าชำระงวดแรก รวมดอกเบี้ยค่างวด - ดอกเบี้ยงวดแรก
                   //             // ถ้างวดต่อไป ดอกเบี้ยคงเหลืองวดก่อน - ดอกเบี้ยค่างวด ปัจจุบัน
                   //             item.InterestPrincipalRemain = (item.InstalmentNo == 1)
                   //                 ? interest.totalInterest - item.InterestInstalment
                   //                 : preItem.InterestPrincipalRemain - item.InterestInstalment;

                   //             item.DiscountInterest = (decimal)item.InterestPrincipalRemain * (decimal)0.5;
                   //         }
                   //     }
                   // }

                   // ctx.UpdateRange(contractItem);
                   //await ctx.SaveChangesAsync();

                    //// เช็คว่า มีการชำระครบหรือยัง
                    //var term = ctx.CreditContractItem
                    //              .Where(p =>
                    //                     p.ContractId == contract.ContractId &&
                    //                     p.RefNo == contract.RefNo &&
                    //                     p.InstalmentNo > 0 &&
                    //                     p.PayDate != null)
                    //               .GroupBy(o => new { o.ContractId })
                    //              .Select(g => new
                    //              {
                    //                  totalPaynetPrice = g.Sum(x => x.PayNetPrice)
                    //              }).SingleOrDefault();
                    ////.GroupBy(x => x.PayNetPrice, (key, values) => new
                    ////{
                    ////    totalPaynetPrice = values.Sum(x => x.PayNetPrice)
                    ////}).FirstOrDefault();


                    //if (term != null && term.totalPaynetPrice >= calculate.Remain)
                    //{
                    //    // ถ้าชำระครบ จะเปลี่ยนสถานะเป็น ชำระครบรอโอนทะเบียน
                    //    contract.ContractStatus = 30;
                    //    contract.EndContractDate = DateTime.Now.Date;

                    //    ctx.Update(contract);
                    //   await ctx.SaveChangesAsync();
                    //}

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
        public IActionResult CancelItemPayment([FromBody] ICancelPayment cencel)
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

        public interface ICancelPayment
        {
            int ContractItemId { get; set; }
            string Remark { get; set; }
            int UpdateBy { get; set; }
        }

        public interface ICreditContractItem
        {
            int ContractItemId { get; set; }
            int ContractId { get; set; }
            string TaxInvoiceNo { get; set; }
            int InstalmentNo { get; set; }
            DateTime DueDate { get; set; }
            DateTime PayDate { get; set; }
            decimal BalanceNetPrice { get; set; }
            decimal PayNetPrice { get; set; }
            int PaymentType { get; set; }
            decimal? FineSum { get; set; }
            decimal? FineSumRemain { get; set; }
            decimal? FineSumeOther { get; set; }
            string Remark { get; set; }
            int Payeer { get; set; }
            int Status { get; set; }
            decimal RemainNetPrice { get; set; }
        }

        public interface IPayment
        {
            int ContractId { get; set; }
            decimal? FineSum { get; set; }
            decimal? FineSumOther { get; set; }
            decimal? PayNetPrice { get; set; }
            decimal? DisCountPrice { get; set; }
            decimal? DiscountRate { get; set; }
            int PaymentType { get; set; }
            string BankCode { get; set; }
            string DocumentRef { get; set; }
            string Remark { get; set; }
            DateTime PayDate { get; set; }
            int BranchId { get; set; }
            int UpdateBy { get; set; }
            ICreditContractItem[] CreditContractItem { get; set; }
        }
    }
}
