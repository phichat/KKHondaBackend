using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using KKHondaBackend.Services;
using KKHondaBackend.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KKHondaBackend.Controllers.Credits
{
  //   [ApiController]
  [Produces("application/json")]
  [Route("api/Credit/Contract/[controller]")]
  public class CreditPaymentController : Controller
  {
    private readonly dbwebContext ctx;
    private readonly ISysParameterService iSysParamService;
    private readonly IStatusService iStatusService;
    private readonly IBankingService iBankingService;
    private readonly ICustomerServices iCustomerService;
    private readonly ISaleReceiptService iSaleReceipt;
    private readonly ISaleTaxService iSaleTax;

    public CreditPaymentController(
        dbwebContext _ctx,
        ISysParameterService isysParamService,
        IStatusService istatusService,
        IBankingService ibankingService,
        ICustomerServices icustService,
        ISaleReceiptService _iSaleReceipt,
        ISaleTaxService _iSaleTax
    )
    {
      ctx = _ctx;
      iSysParamService = isysParamService;
      iStatusService = istatusService;
      iBankingService = ibankingService;
      iCustomerService = icustService;
      iSaleReceipt = _iSaleReceipt;
      iSaleTax = _iSaleTax;
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

                        join _sale in ctx.User on db.CreateBy equals _sale.Id into a14
                        from sale in a14.DefaultIfEmpty()

                        where db.ContractId == id

                        select new
                        {
                          ContractId = id,
                          db.SaleId,
                          db.BookingId,
                          db.RefNo,
                          db.ContractNo,
                          db.ContractDate,
                          db.ContractStatus,
                          status.StatusDesc,
                          ContractHire = $"{contrachHire.CustomerPrename}{contrachHire.CustomerName} {contrachHire.CustomerSurname}",
                          SaleName = sale.FullName,
                          db.Remark
                        }).FirstOrDefault();

        if (contract == null)
          return StatusCode(404);


        var calculate = ctx.Sale.SingleOrDefault(p => p.SaleId == contract.SaleId);

        if (calculate == null)
          return StatusCode(400);


        var _contractItem = ctx.CreditContractItem
                           .Where(x => x.ContractId == id &&
                           x.RefNo == contract.RefNo
                           && x.InitialPrice >= (decimal)0
                           ).OrderBy(x => x.InstalmentNo).ToList();

        if (_contractItem == null)
          return StatusCode(400);

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

        if (booking.BookingPaymentType == BookingPaymentType.HierPurchase)
        {
          _contractItem.ForEach(x =>
          {
            var DueDate = (DateTime)x.DueDate;
            var DelayDue = (int)(DateTime.Now.Date - DueDate).TotalDays;
            if (DelayDue > 0 && x.Status == 13)
            {
              x.FineSum = 100;
              x.FineSumRemain = 100;
              x.DelayDueDate = DelayDue;
              x.CheckDueDate = (DateTime.Now.Date);
            }
          });
          ctx.CreditContractItem.UpdateRange(_contractItem);
          ctx.SaveChanges();
        }

        var isPay = _contractItem.Where(x => x.InstalmentNo > 0 && x.RemainNetPrice == 0)
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
                              db.ContractId,
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
                    {"sale", calculate},
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

    [HttpGet("GetReceiptByContractId")]
    public async Task<IActionResult> GetReceiptByContractId(int contractId)
    {
      var list = await (from hd in ctx.CreditTransactionH
                        where hd.ContractId == contractId && hd.Status == 11
                        select new CreditTransactionReceipt
                        {
                          TaxInvNo = hd.TaxInvNo,
                          TaxInvStatus = hd.TaxInvStatus,
                          ReceiptNo = hd.ReceiptNo,
                          ReceiptStatus = hd.ReceiptStatus
                        })
                        .Distinct()
                        .ToListAsync();
      return Ok(list);
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
          var calculate = ctx.Sale.FirstOrDefault(p => p.SaleId == contract.SaleId);
          // true ปิดบันชีเช่าซื้อ
          // false ชำระค่างวดปกติ
          var totalPaymentPrice = payment.CutBalance > 0
            ? payment.Outstanding
            : (payment.TotalPaymentPrice - payment.FineSumeOther) - payment.RevenueStamp;

          var _contractItem = ctx.CreditContractItem
                  .Where(x => x.ContractId == payment.ContractId && x.Status != 11)
                  .OrderBy(x => x.InstalmentNo)
                  .ToList();

          var cHire = iCustomerService.GetCustomerByCode(contract.ContractHire).Result;
          var tax = iSaleTax.SetSaleTax(cHire, payment.BranchId);
          tax.TaxNo = iSysParamService.GenerateTaxInvNo(payment.BranchId);
          ctx.SaleTax.Add(tax);
          ctx.SaveChanges();

          var receiptNo = iSaleReceipt.SetSaleReceipt(cHire, payment.BranchId);
          receiptNo.ReceiptNo = iSysParamService.GenerateReceiptNo(payment.BranchId);
          ctx.SaleReceipt.Add(receiptNo);
          ctx.SaveChanges();

          var CreditTransDTList = new List<CreditTransactionD>();
          var CreditTransHD = new CreditTransactionH
          {
            ContractId = payment.ContractId,
            ReceiptNo = receiptNo.ReceiptNo,
            TaxInvNo = tax.TaxNo,
            AccBankId = payment.AccBankId,
            Payeer = payment.Payeer,
            PaymentType = payment.PaymentType,
            DocumentRef = payment.DocumentRef,
            BranchId = payment.BranchId,
            PaymentName = payment.PaymentName,
            OutstandingBalance = payment.Outstanding,
            DiscountInterest = payment.DiscountInterest,
            CutBalance = payment.CutBalance,
            PaymentPrice = payment.PaymentPrice,
            DiscountPrice = payment.DiscountPrice,
            TotalPaymentPrice = payment.TotalPrice,
            PaymentDate = payment.PaymentDate,
            Remark = payment.Remark,
            Status = payment.CutBalance > 0 ? 25 : 11,
            CreateDate = DateTime.Now,
            CreateBy = payment.Payeer
          };

          ctx.CreditTransactionH.Add(CreditTransHD);
          ctx.SaveChanges();

          _contractItem.ForEach(Item =>
           {
             if (totalPaymentPrice <= 0) return;

             var CreditTransDT = new CreditTransactionD
             {
               CTH_Id = CreditTransHD.CTH_Id,
               ContractItemId = Item.ContractItemId,
               RevenueStamp = payment.RevenueStamp,
               DelayDueDate = Item.DelayDueDate,
             };

             // รวมยอดชำระจากรอบบิลก่อนเพื่อนำมาตรวจสอบ
             // 1. กรณีมียอดค้างจากรอบบิลก่อน
             //    1.1 หักเงินค่าปรับ
             //    1.2 หักค่างวด
             // 2. กรณีที่ไม่มียอดค้างจากรอบบิลก่อน 
             var oldTrans = (from d in ctx.CreditTransactionD
                             join h in ctx.CreditTransactionH on d.CTH_Id equals h.CTH_Id
                             where d.ContractItemId == Item.ContractItemId && h.Status == 11
                             group d by new { d.ContractItemId } into g
                             select new
                             {
                               ContractItemId = g.Key,
                               PayNetPrice = g.Sum(x => x.PayNetPrice),
                               FineSum = g.Sum(x => x.FineSum),
                               Count = g.Count()
                             })
                              .AsNoTracking()
                              .SingleOrDefault();

             if (oldTrans != null)
             {
               // ตัดยอดชำระค่าปรับ ก่อน ค่างวด
               if (Item.FineSumRemain > oldTrans.FineSum && totalPaymentPrice > 0)
               {
                 var fineSum = ((decimal)Item.FineSumRemain - oldTrans.FineSum);
                 Item.FineSumStatus = 11;
                 Item.FineSumRemain -= fineSum;
                 totalPaymentPrice -= fineSum;
                 CreditTransDT.FineSum = fineSum;
               }
               // ตัดยอดชำระค่างวด
               if (Item.PayNetPrice > oldTrans.PayNetPrice && totalPaymentPrice > 0)
               {
                 var outStanding = ((decimal)Item.PayNetPrice - (decimal)oldTrans.PayNetPrice);
                 Payment(ref totalPaymentPrice, ref CreditTransDT, ref Item, outStanding);
               }
             }
             else
             {
               // ตัดยอดชำระค่าปรับ ก่อน ค่างวด
               if (Item.FineSumRemain > 0 && totalPaymentPrice > 0)
               {
                 var fineSum = Item.FineSumRemain;
                 Item.FineSumStatus = 11;
                 Item.FineSumRemain -= fineSum;
                 totalPaymentPrice -= fineSum;
                 CreditTransDT.FineSum = fineSum;
               }
               // ตัดยอดชำระค่างวด
               Payment(ref totalPaymentPrice, ref CreditTransDT, ref Item, (decimal)Item.PayNetPrice);
             }

             int paymentCount = oldTrans != null ? oldTrans.Count + 1 : 1;
             CreditTransDT.Description = Item.InstalmentNo == 0 ? "เงินดาวน์" : $"ค่างวดที่ {Item.InstalmentNo}/{paymentCount}";
             CreditTransDTList.Add(CreditTransDT);

             Item.UpdateBy = payment.Payeer;
             Item.UpdateDate = DateTime.Now;
             ctx.CreditContractItem.Update(Item);
             ctx.SaveChanges();
           });

          ctx.CreditTransactionD.AddRange(CreditTransDTList);
          ctx.SaveChanges();

          // นับจำนวนรายการชำระครบ
          var isPay = ctx.CreditContractItem
                  .Where(p =>
                      p.Status == 11 &&
                      p.ContractId == contract.ContractId &&
                      p.RefNo == contract.RefNo)
                  .Count();

          // นับจำนวนทั้งหมด
          var totalRec = ctx.CreditContractItem
                  .Where(p =>
                      p.ContractId == contract.ContractId &&
                      p.RefNo == contract.RefNo)
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
    private void Payment(ref decimal totalPaymentPrice, ref CreditTransactionD creditTransDT, ref CreditContractItem item, decimal outStanding)
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

      creditTransDT.PayNetPrice = payNetPrice;
      creditTransDT.PayVatPrice = payVatPrice;
      creditTransDT.PayPrice = payPrice;

      item.Remain -= payPrice;
      item.RemainVatPrice -= payVatPrice;
      item.RemainNetPrice -= payNetPrice;
    }

    // DELETE: api/ApiWithActions/5
    [HttpPost("[Action]")]
    public IActionResult CancelReceiptNo([FromBody] ICancelPayment cancel)
    {
      using (var transaction = ctx.Database.BeginTransaction())
      {
        try
        {
          var cthd = ctx.CreditTransactionH.Where(e =>
            e.ContractId == cancel.ContractId &&
            e.ReceiptNo == cancel.SlipNo &&
            e.Status != 13);

          if (!cthd.Any())
          {
            return NotFound();
          }

          var receipt = ctx.SaleReceipt.Where(x => x.ReceiptNo == cancel.SlipNo).Single();
          receipt.Status = false;
          receipt.Reason = cancel.Reason;
          receipt.ApproveId = cancel.ApproveBy;
          receipt.UpdateBy = cancel.UpdateBy;
          receipt.UpdateDate = DateTime.Now;
          ctx.SaleReceipt.Update(receipt);
          ctx.SaveChanges();

          var hd = cthd.Single();
          hd.ReceiptStatus = false;
          hd.UpdateBy = cancel.UpdateBy;
          hd.UpdateDate = DateTime.Now;
          if (hd.TaxInvStatus == false && hd.ReceiptStatus == false)
          {
            hd.Status = 0;

            var cItem = SetCreditContractItem(hd.CTH_Id, cancel.UpdateBy);
            ctx.UpdateRange(cItem);
            ctx.SaveChanges();

            var ct = ctx.CreditContract.SingleOrDefault(x => x.ContractId == cancel.ContractId);
            // เปลี่ยนสถานะ อยู่ระหว่างการผ่อนชำระ
            ct.ContractStatus = 31;
            ct.EndContractDate = DateTime.Now;
            ctx.CreditContract.Update(ct);
            ctx.SaveChanges();
          }

          ctx.CreditTransactionH.Update(hd);
          ctx.SaveChanges();
          transaction.Commit();

          return NoContent();
        }
        catch (Exception ex)
        {
          transaction.Rollback();
          return StatusCode(500, ex.Message);
        }
      }
    }

    [HttpPost("[Action]")]
    public IActionResult CancelTaxInvNo([FromBody] ICancelPayment cancel)
    {
      using (var transaction = ctx.Database.BeginTransaction())
      {
        try
        {
          var cthd = ctx.CreditTransactionH.Where(e =>
            e.ContractId == cancel.ContractId &&
            e.TaxInvNo == cancel.SlipNo &&
            e.Status != 13);

          if (!cthd.Any())
          {
            return NotFound();
          }

          var taxInv = ctx.SaleTax.Where(x => x.TaxNo == cancel.SlipNo).Single();
          taxInv.Status = false;
          taxInv.Reason = cancel.Reason;
          taxInv.ApproveId = cancel.ApproveBy;
          taxInv.UpdateBy = cancel.UpdateBy;
          taxInv.UpdateDate = DateTime.Now;
          ctx.SaleTax.Update(taxInv);
          ctx.SaveChanges();

          var hd = cthd.Single();
          hd.TaxInvStatus = false;
          hd.UpdateBy = cancel.UpdateBy;
          hd.UpdateDate = DateTime.Now;

          if (hd.TaxInvStatus == false && hd.ReceiptStatus == false)
          {
            hd.Status = 0;
            var cItem = SetCreditContractItem(hd.CTH_Id, cancel.UpdateBy);
            ctx.UpdateRange(cItem);
            ctx.SaveChanges();

            var ct = ctx.CreditContract.SingleOrDefault(x => x.ContractId == cancel.ContractId);
            // เปลี่ยนสถานะ อยู่ระหว่างการผ่อนชำระ
            ct.ContractStatus = 31;
            ct.EndContractDate = DateTime.Now;
            ctx.CreditContract.Update(ct);
            ctx.SaveChanges();
          }

          ctx.CreditTransactionH.Update(hd);
          ctx.SaveChanges();
          transaction.Commit();

          return NoContent();
        }
        catch (Exception ex)
        {
          transaction.Rollback();
          return StatusCode(500, ex.Message);
        }
      }
    }

    private List<CreditContractItem> SetCreditContractItem(int cTH_Id, int updateBy)
    {
      var ctdt = ctx.CreditTransactionD.Where(e => e.CTH_Id == cTH_Id);
      var cItem = new List<CreditContractItem>();
      foreach (var dt in ctdt)
      {
        var item = ctx.CreditContractItem.Single(_item => _item.ContractItemId == dt.ContractItemId);
        item.Remain += dt.PayPrice;
        item.RemainVatPrice += dt.PayVatPrice;
        item.RemainNetPrice += dt.PayNetPrice;
        item.FineSumRemain += dt.FineSum;
        item.FineSumOther += dt.FineSumOther;
        item.FineSumStatus = null;
        item.Status = item.RemainNetPrice < item.PayNetPrice ? 12 : 13;
        item.UpdateBy = updateBy;
        item.UpdateDate = DateTime.Now;
        cItem.Add(item);
      }
      return cItem;
    }

    public class ICancelPayment
    {
      public int ContractId { get; set; }
      public string SlipNo { get; set; }
      public string Reason { get; set; }
      public int UpdateBy { get; set; }
      public int ApproveBy { get; set; }
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
      public int ContractId { get; set; }
      public decimal Outstanding { get; set; }
      public List<int> InstalmentNo { get; set; }
      public int? DueDate { get; set; }
      public decimal CutBalance { get; set; }
      public decimal DiscountInterest { get; set; }
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
