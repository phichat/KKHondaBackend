using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using KKHondaBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace KKHondaBackend.Controllers.Credits
{
  //   [ApiController]
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
        // var bankingsDropdown = iBankingService.GetDropdowns();

        // var statusDropdown = iStatusService.GetDropdown();
        // statusDropdown = statusDropdown
        //     .Where(db => db.Value == "10" || db.Value == "11" || db.Value == "12" || db.Value == "13")
        //     .ToArray();

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
                          ContractHire = $"{contrachHire.CustomerPrename}{contrachHire.CustomerName} {contrachHire.CustomerSurname}",
                          SaleName = sale.FullName,
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
                           ).OrderBy(x => x.InstalmentNo).ToList();

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

                       join book in ctx.Booking on item.BookingId equals book.BookingId into _book
                       from __book in _book.DefaultIfEmpty()

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
                         __book.BookingPaymentType,
                         DepositIsPay = deposit.Status != 13 ? deposit.RemainNetPrice : 0,
                         DepositIsOutstanding = deposit.Status == 13 ? deposit.RemainNetPrice : 0,
                       }).FirstOrDefault();

        var isPay = _contractItem.Where(x => x.InstalmentNo > 0 && x.RemainNetPrice == 0)
                       .GroupBy(o => new { o.ContractId })
                       .Select(g => new
                       {
                         IsPayPrice = g.Sum(x => x.PayNetPrice),
                         IsPayTerm = g.Count()
                       }).FirstOrDefault();

        // var isPay = ctx.CreditContractPayment.Where(x => x.ContractId == id && x.InstalmentNo > 0)
        //     .GroupBy(x => x.ContractId)
        //     .Select(x => new {
        //         IsPayPrice = x.Sum(o => o.PayNetPrice),
        //         IsPayTerm = x.Count()
        //     }).FirstOrDefault();


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
                              db.Remark,
                              db.RemainNetPrice,
                              db.Status,
                              s.StatusDesc
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

    private static double CheckFineSum(DateTime DueDate)
    {
      var Compare = DueDate.CompareTo(DateTime.Now.Date);
      int DelayDue = (int)(DateTime.Now.Date - DueDate).TotalDays;
      return Compare < 0 ? DelayDue * 100 : 0;
    }


    // PUT: api/CreditPayment/5
    [HttpPost("PaymentTerm")]
    public IActionResult PaymentTerm([FromBody] IPayment payment)
    {
      if (payment == null) return BadRequest();

      using (var transaction = ctx.Database.BeginTransaction())
      {
        try
        {
          var contract = ctx.CreditContract.FirstOrDefault(p => p.ContractId == payment.ContractId);
          var calculate = ctx.CreditCalculate.FirstOrDefault(p => p.CalculateId == contract.CalculateId);
          var totalPaymentPrice = (payment.TotalPaymentPrice - payment.FineSumeOther) - payment.RevenueStamp;
          // var instalNo = payment.InstalmentNo;  
          var _contractItem = ctx.CreditContractItem
                  .Where(x => x.ContractId == payment.ContractId && x.Status != 11)
                  .OrderBy(x => x.InstalmentNo)
                  .ToList();

          var taxInvoiceNo = iSysParamService.GenerateHpsTransactionTaxInvNo(payment.BranchId);
          var receiptNo = iSysParamService.GenerateHpsTransactionReceiptNo(payment.BranchId);
          var CreditTransList = new List<CreditTransaction>();

          var instalNo = new List<int>();

          _contractItem.ForEach(Item =>
          {
            if (totalPaymentPrice <= 0) return;

            instalNo.Add(Item.InstalmentNo);
            var CreditTransItem = new CreditTransaction
            {
              ReceiptNo = receiptNo,
              TaxInvNo = taxInvoiceNo,
              ContractItemId = Item.ContractItemId,
              RevenueStamp = payment.RevenueStamp,
              PaymentName = payment.PaymentName,
              DelayDueDate = Item.DelayDueDate,
              AccBankId = payment.AccBankId,
              Payeer = payment.Payeer,
              PaymentType = payment.PaymentType,
              DocumentRef = payment.DocumentRef,
              PaymentPrice = payment.PaymentPrice,
              DiscountPrice = payment.DiscountPrice,
              TotalPaymentPrice = payment.TotalPaymentPrice,
              PaymentDate = payment.PaymentDate,
              BranchId = payment.BranchId,
              Remark = payment.Remark,
              CreateDate = DateTime.Now,
              CreateBy = payment.Payeer,
              UpdateDate = DateTime.Now,
              UpdateBy = payment.UpdateBy
            };

            // รวมยอดชำระจากรอบบิลก่อนเพื่อนำมาตรวจสอบ
            // 1. กรณีมียอดค้างจากรอบบิลก่อน
            //    1.1 หักเงินค่าปรับ
            //    1.2 หักค่างวด
            // 2. กรณีที่ไม่มียอดค้างจากรอบบิลก่อน 
            var oldTrans = (from t in ctx.CreditTransactions
                            where t.ContractItemId == Item.ContractItemId
                            group t by new { t.ContractItemId } into g
                            select new
                            {
                              ContractItemId = g.Key,
                              PayNetPrice = g.Sum(x => x.PayNetPrice),
                              FineSum = g.Sum(x => x.FineSum)
                            })
                            .AsNoTracking()
                            .SingleOrDefault();

            if (oldTrans != null)
            {
              // ตัดยอดชำระค่าปรับ ก่อน ค่างวด
              if (Item.FineSumRemain > oldTrans.FineSum && totalPaymentPrice > 0)
              {
                var fineSum = ((decimal)Item.FineSumRemain - oldTrans.FineSum);
                Item.FineSumRemain -= fineSum;
                totalPaymentPrice -= fineSum;
                CreditTransItem.FineSum = fineSum;
              }
              // ตัดยอดชำระค่างวด
              if (Item.PayNetPrice > oldTrans.PayNetPrice && totalPaymentPrice > 0)
              {
                var outStanding = ((decimal)Item.PayNetPrice - (decimal)oldTrans.PayNetPrice);
                Payment(ref totalPaymentPrice, ref CreditTransItem, ref Item, outStanding);
              }
            }
            else
            {
              // ตัดยอดชำระค่าปรับ ก่อน ค่างวด
              if (Item.FineSumRemain > 0 && totalPaymentPrice > 0)
              {
                var fineSum = Item.FineSumRemain;
                Item.FineSumRemain -= fineSum;
                totalPaymentPrice -= fineSum;
                CreditTransItem.FineSum = fineSum;
              }
              // ตัดยอดชำระค่างวด
              Payment(ref totalPaymentPrice, ref CreditTransItem, ref Item, (decimal)Item.PayNetPrice);
            }

            CreditTransList.Add(CreditTransItem);

            ctx.CreditContractItem.Update(Item);
            ctx.SaveChanges();
          });

          int index = instalNo.Count - 1;
          var firstIndex = instalNo[0] == 0 ? "เงินดาวน์" : $"ค่างวดท่ี {instalNo[0].ToString()}";
          CreditTransList.ForEach(x =>
          {
            x.Description = instalNo.Count > 1 ? $"{firstIndex} - งวดที่ {instalNo[index]}" : firstIndex;
          });

          ctx.CreditTransactions.AddRange(CreditTransList);
          ctx.SaveChanges();

          // นับจำนวนรายการชำระครบ
          var isPay = ctx.CreditContractItem
                  .Where(p =>
                      p.Status == 11 &&
                      p.ContractId == contract.ContractId &&
                      p.RefNo == contract.RefNo)
                  .AsNoTracking()
                  .Count();

          // นับจำนวนทั้งหมด
          var totalRec = ctx.CreditContractItem
                  .Where(p =>
                      p.ContractId == contract.ContractId &&
                      p.RefNo == contract.RefNo)
                  .AsNoTracking()
                  .Count();

          if (isPay == totalRec)
          {
            // ถ้าชำระครบ จะเปลี่ยนสถานะเป็น ชำระครบรอโอนทะเบียน
            contract.ContractStatus = 30;
            contract.EndContractDate = DateTime.Now.Date;
            ctx.CreditContract.Update(contract);
            ctx.SaveChanges();
          }

          transaction.Commit();

          // return Get(payment.ContractId);
          return NoContent();

        }
        catch (Exception ex)
        {
          Console.Write(ex.Message);
          transaction.Rollback();
          return StatusCode(500, ex.Message);
        }
      }
    }
    private void Payment(ref decimal totalPaymentPrice, ref CreditTransaction creditTransItem, ref CreditContractItem item, decimal outStanding)
    {
      var vat = (decimal)((item.VatRate / 100) + 1);
      // ยอดหักตามจริง
      decimal payNetPrice;
      if (totalPaymentPrice >= outStanding)
      {
        payNetPrice = outStanding;
        totalPaymentPrice -= outStanding;
        item.Status = 11; // ชำระครบ
      }
      else
      {
        payNetPrice = totalPaymentPrice;
        totalPaymentPrice = 0;
        item.Status = 12; // ชำระบางส่วน
      };
      var payPrice = Math.Round(payNetPrice / vat, 4);
      var payVatPrice = Math.Round(payNetPrice - payPrice, 4);

      creditTransItem.PayNetPrice = payNetPrice;
      creditTransItem.PayVatPrice = payVatPrice;
      creditTransItem.PayPrice = payPrice;

      item.Remain -= payPrice;
      item.RemainVatPrice -= payVatPrice;
      item.RemainNetPrice -= payNetPrice;
      item.UpdateBy = creditTransItem.Payeer;
      item.UpdateDate = DateTime.Now;
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
        item.Remain = item.Balance;
        item.RemainVatPrice = item.BalanceVatPrice;
        item.RemainNetPrice = item.BalanceNetPrice;
        item.FineSumRemain = item.FineSum;
        item.FineSumOther = 0;
        if (item.FineSumStatus != null) item.FineSumStatus = 13;
        item.Status = 13;
        item.BankCode = null;
        item.TaxInvoiceNo = null;
        item.ReceiptNo = null;
        item.CancelRemark = cencel.CancelRemark;
        item.UpdateBy = cencel.UpdateBy;
        item.UpdateDate = DateTime.Now;
        ctx.SaveChanges();

        // ลบรายการ transaction การชำระเงินใน งวดนั้น
        var ContractTrans = ctx.CreditTransactions.Where(e => e.ContractItemId == cencel.ContractItemId).ToList();
        ctx.RemoveRange(ContractTrans);
        ctx.SaveChanges();

        // นับจำนวนรายการชำระครบ
        var isPay = ctx.CreditContractItem
                .Where(p =>
                    p.Status == 11 &&
                    p.ContractId == item.ContractId &&
                    p.RefNo == item.RefNo)
                .Count();

        // นับจำนวนทั้งหมด
        var totalRec = ctx.CreditContractItem
                .Where(p =>
                    p.ContractId == item.ContractId &&
                    p.RefNo == item.RefNo)
                .Count();

        var ct = ctx.CreditContract.SingleOrDefault(x => x.ContractId == item.ContractId);

        if (isPay < totalRec)
        {
          // เปลี่ยนสถานะ อยู่ระหว่างการผ่อนชำระ
          ct.ContractStatus = 31;
          ct.EndContractDate = DateTime.Now;
          ctx.CreditContract.Update(ct);
          ctx.SaveChanges();
        }

        return Get(item.ContractId);
      }
      catch (Exception)
      {
        return StatusCode(500);
      }
    }

    public class ICancelPayment
    {
      public int ContractItemId { get; set; }
      public string CancelRemark { get; set; }
      public int UpdateBy { get; set; }
    }

    public class ICreditContractItem
    {
      public int ContractItemId { get; set; }
      public int ContractId { get; set; }
      public int InstalmentNo { get; set; }
      public decimal? FineSum { get; set; }
      public decimal? FineSumRemain { get; set; }
    }

    public class IPayment
    {
      // public int ContractId { get; set; }
      // public decimal? FineSum { get; set; }
      // public decimal? FineSumOther { get; set; }
      // public decimal? PayNetPrice { get; set; }
      // public decimal? DisCountPrice { get; set; }
      // public decimal? DiscountRate { get; set; }
      // public decimal? RevenueStamp { get; set; }
      // public int PaymentType { get; set; }
      // public string BankCode { get; set; }
      // public string DocumentRef { get; set; }
      // public string Remark { get; set; }
      // public DateTime PayDate { get; set; }
      // public int BranchId { get; set; }
      // public int UpdateBy { get; set; }

      public int ContractId { get; set; }
      public decimal Outstanding { get; set; }
      public List<int> InstalmentNo { get; set; }
      public int? DueDate { get; set; }
      public string PaymentName { get; set; }
      public decimal RevenueStamp { get; set; }
      public decimal PayNetPrice { get; set; }
      public decimal FineSume { get; set; }
      public decimal FineSumeOther { get; set; }
      public int Payeer { get; set; }
      public decimal BalanceNetPrice { get; set; }
      public string Remark { get; set; }
      public int UpdateBy { get; set; }
      public int BranchId { get; set; }
      public decimal TotalPrice { get; set; }
      public int? Status { get; set; }
      public int PaymentType { get; set; }
      public decimal PaymentPrice { get; set; }
      public decimal DiscountPrice { get; set; }
      public decimal TotalPaymentPrice { get; set; }
      public int? AccBankId { get; set; }
      public DateTime PaymentDate { get; set; }
      public string DocumentRef { get; set; }
      public CreditContractItem[] CreditContractItem { get; set; }
    }
  }

}
