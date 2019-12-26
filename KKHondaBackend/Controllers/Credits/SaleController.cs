using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using KKHondaBackend.Services;
using KKHondaBackend.Entities;

namespace KKHondaBackend.Controllers.Credits
{
  //   [ApiController]
  [Produces("application/json")]
  [Route("api/Credit/[controller]")]
  public class SaleController : Controller
  {
    private readonly dbwebContext _context;
    private readonly IBookingServices iBookService;
    private readonly ISysParameterService iSysParamService;
    private readonly ICustomerServices iCustomerService;
    private readonly IReserveReturnService iReserveReturn;
    private readonly ISaleCommissionService iSaleCommission;
    private readonly ISaleInvTaxRecService iSaleInvTaxRec;
    private readonly ISaleReceiptService iSaleReceipt;

    public SaleController(
        dbwebContext context,
        IBookingServices iBookingService,
        ISysParameterService isysParamService,
        ICustomerServices icustService,
        IReserveReturnService _iReserveReturn,
        ISaleCommissionService _iSaleCommission,
        ISaleInvTaxRecService _iSaleInvTaxRec,
        ISaleReceiptService _iSaleReceipt
    )
    {
      _context = context;
      iBookService = iBookingService;
      iSysParamService = isysParamService;
      iCustomerService = icustService;
      iReserveReturn = _iReserveReturn;
      iSaleCommission = _iSaleCommission;
      iSaleInvTaxRec = _iSaleInvTaxRec;
      iSaleReceipt = _iSaleReceipt;
    }

    // GET: api/Sale
    [HttpGet]
    public IEnumerable<Sale> GetSale()
    {
      return _context.Sale;
    }

    // GET: api/Sale/5
    [HttpGet("GetById")]
    public IActionResult GetSale(int SaleId)
    {

      var calc = _context.Sale
                         .Where(p => p.SaleId == SaleId)
                         .SingleOrDefault();

      var cont = _context.CreditContract
                         .Where(p => p.SaleId == SaleId)
                         .SingleOrDefault();

      var contItem = _context.CreditContractItem
                             .Where(p => p.ContractId == cont.ContractId && p.RefNo == cont.RefNo)
                             .OrderBy(o => o.InstalmentNo)
                             .ToList();

      var book = iBookService.GetBookingById(calc.BookingId);

      var obj = new Dictionary<string, object>
            {
                {"sale", calc},
                {"creditContract", cont},
                {"creditContractItem", contItem},
                {"booking", book}
            };

      return Ok(obj);
    }

    // PUT: api/Sale/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSale([FromRoute] int id, [FromBody] Sale Sale)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != Sale.SaleId)
      {
        return BadRequest();
      }

      _context.Entry(Sale).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!SaleExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    [HttpGet("GetEngineByKeyword")]
    public IActionResult GetEngineByKeyword(int bookingId, int branchId, string term)
    {

      try
      {
        var bookItem = _context.BookingItem.SingleOrDefault(p => p.BookingId == bookingId && p.ItemDetailType == 1);
        var model = _context.ProductModel.SingleOrDefault(p => p.ModelId == bookItem.ModelId);
        var stockReceive = (from stock in _context.StockReceive
                            join log in _context.TransferLog on
                                new { p1 = stock.ItemId, p2 = stock.BranchId } equals
                                new { p1 = log.ItemId, p2 = log.ReceiverId }
                            orderby log.EngineNo ascending

                            where stock.ItemId == bookItem.ItemId &&
                            stock.BranchId == branchId &&
                            stock.StockAviable > 0 &&
                            stock.StockOnhand > 0 &&
                            log.BQty > 0 &&
                            log.LogStatus == 2 &&
                            (log.EngineNo.Contains(term) || log.FrameNo.Contains(term))

                            select new ModelEngine
                            {
                              LogId = log.LogId,
                              Model = model.ModelCode,
                              EngineNo = log.EngineNo,
                              FrameNo = log.FrameNo
                            }).ToList();

        return Ok(stockReceive);

      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }

    }

    [HttpPost("Create")]
    public IActionResult Create([FromBody] Credit credit)
    {
      // [FromBody] Credit credit
      return Created(credit);
      // return Ok(credit);
    }

    // [HttpPost("Edit")]
    // public IActionResult Edit([FromBody] Credit credit)
    // {
    //   return Edited(credit);
    // }

    // [HttpPost("Revice")]
    // public IActionResult Revice([FromBody] Credit credit)
    // {
    //   return Reviced(credit);
    // }

    private IActionResult Created(Credit credit)
    {
      using (var transaction = _context.Database.BeginTransaction())
      {
        var calculate = new Sale();
        var contract = new CreditContract();
        var contractItems = new List<CreditContractItem>();
        try
        {
          var reserve = credit.sale;
          calculate = credit.sale;
          contract = credit.creditContract;
          contractItems = credit.creditContactItem.ToList();

          // Booking
          var booking = _context.Booking.Where(x => x.BookingId == calculate.BookingId).Single();
          booking.BookingStatus = 2; // สถานะขาย
          _context.Update(booking);
          _context.SaveChanges();

          var cBooking = iCustomerService.GetCustomerByCode(booking.CustomerCode).Result;
          var cHire = cBooking;
          if (booking.CustomerCode != contract.ContractHire)
            cHire = iCustomerService.GetCustomerByCode(contract.ContractHire).Result;

          if (reserve.ReturnDeposit == 1 && reserve.ReturnDepositPrice > 0)
          {
            // กรณีมีการคืนเงินมัดจำ
            var deposit = iReserveReturn.SetReserve(reserve, cBooking);
            deposit.ReturnDepositNo = iSysParamService.GeerateeReturnDepositNo((int)contract.BranchId);
            // ใบคืนเงินมัดจำ
            calculate.ReturnDepositNo = deposit.ReturnDepositNo;
            _context.ReserveReturn.Add(deposit);
            _context.SaveChanges();
          }

          if (reserve.ComPrice > 0)
          {
            // ใบเสร็จค่าคอมมิชั่น
            var saleCom = iSaleCommission.SetSaleCommission(reserve, cHire, (int)contract.BranchId);
            saleCom.ComNo = iSysParamService.GenerateTaxInvNo((int)contract.BranchId);
            calculate.ComNo = saleCom.ComNo;
            _context.SaleCommission.Add(saleCom);
            _context.SaveChanges();
          }

          // ใบส่งของ/ใบกำกับภาษี/ใบเสร็จรับเงิน
          var invTaxRec = iSaleInvTaxRec.SetSaleInvTaxRec(cHire, (int)contract.BranchId);
          invTaxRec.InvTaxRecNo = iSysParamService.GenerateVatNo((int)contract.BranchId);
          calculate.InvTaxRecNo = invTaxRec.InvTaxRecNo;
          _context.SaleInvTaxRec.Add(invTaxRec);
          _context.SaveChanges();

          switch (booking.BookingPaymentType)
          {
            case BookingPaymentType.Cash:
              // ใบเสร็จรับเงิน
              var receipt = iSaleReceipt.SetSaleReceipt(cHire, (int)contract.BranchId);
              receipt.ReceiptNo = iSysParamService.GenerateReceiptNo((int)contract.BranchId);
              calculate.ReceiptNo = receipt.ReceiptNo;
              _context.SaleReceipt.Add(receipt);
              _context.SaveChanges();
              // ชำระครบรอโอนทะเบียน
              contract.ContractStatus = 30;
              break;

            case BookingPaymentType.Leasing:
            case BookingPaymentType.Credit:
              contract.ContractStatus = 31;
              break;
          }

          // ใบขาย
          calculate.SellNo = iSysParamService.GenerateSellNo((int)booking.BranchId);
          calculate.InvTaxRecNo = invTaxRec.InvTaxRecNo;
          calculate.SaleDate = DateTime.Now;
          _context.Sale.Add(calculate);
          _context.SaveChanges();

          if (booking.BookingPaymentType == BookingPaymentType.HierPurchase)
          {
            contract.ContractNo = iSysParamService.GenerateContractNo((int)contract.BranchId);
            var owner = _context.Branch
              .Where(x => x.BranchId == (int)contract.BranchId)
              .AsNoTracking()
              .FirstOrDefault();
            contract.OwnerTaxNo = owner.BranchRegisterNo;
          }
          else
          {
            var owner = _context.MCustomer
              .Where(x => x.CustomerCode == contract.ContractOwner)
              .AsNoTracking()
              .FirstOrDefault();
            contract.OwnerTaxNo = owner.IdCard;
          }

          var hAddress = cHire.MCustomerAddress.FirstOrDefault(x => x.AddressDefault == true);
          contract.HireAddress = hAddress.Address;
          contract.HireAmpherCode = hAddress.AmphorCode;
          contract.HireProvinceCode = hAddress.ProvinceCode;
          contract.HireZipCode = hAddress.Zipcode;
          contract.ContractDate = DateTime.Now;
          contract.EndContractDate = contractItems[contractItems.Count - 1].DueDate; 
          contract.SaleId = calculate.SaleId;
          contract.RefNo = GenerateReferenceContract(null);
          contract.CreateDate = DateTime.Now;
          _context.CreditContract.Add(contract);
          _context.SaveChanges();

          if (contractItems.Any())
          {
            // ContractItem
            foreach (var item in contractItems)
            {
              item.ContractId = contract.ContractId;
              item.RefNo = contract.RefNo;
              item.PayPrice = item.Balance;
              item.PayVatPrice = item.BalanceVatPrice;
              item.PayNetPrice = item.BalanceNetPrice;
              item.Status = 13; // สถานะยังไม่ชำระ
              item.CreateBy = contract.CreateBy;
              item.CreateDate = DateTime.Now;
            }
            _context.CreditContractItem.AddRange(contractItems);
            _context.SaveChanges();
          }

          // หลังจากเลือกรายการที่ค้นหาแล้วให้เอา log_id ที่ได้มาเก็บตอนทำการขาย      
          // มีเงื่อนไขโดย itemDetailType==1(สินค้าประเภทรถ)
          var bookItem = _context.BookingItem.FirstOrDefault(x => x.BookingId == contract.BookingId && x.ItemDetailType == 1);
          if (bookItem != null)
          {
            bookItem.LogReceiveId = calculate.LogReceiveId;
            _context.BookingItem.Update(bookItem);
            _context.SaveChanges();
          }

          // ค้นหาและอัพเดท stock ด้วย LogReceiveId(LogIg)
          var tfLog = _context.TransferLog.FirstOrDefault(x => x.LogId == calculate.LogReceiveId);
          var stockReceive = _context.StockReceive.FirstOrDefault(x => x.ItemId == tfLog.ItemId && tfLog.ReceiverId == x.BranchId);

          if (tfLog != null)
          {
            tfLog.BQty -= 1;
            _context.TransferLog.Update(tfLog);
            _context.SaveChanges();
          }

          if (stockReceive != null)
          {
            var viable = stockReceive.StockAviable;
            var onHand = stockReceive.StockOnhand;
            stockReceive.StockAviable -= 1;
            stockReceive.StockOnhand -= 1;
            _context.StockReceive.Update(stockReceive);
            _context.SaveChanges();
          }

          transaction.Commit();
          var obj = new Dictionary<string, object> { { "contractId", contract.ContractId } };
          return Ok(obj);

        }
        catch (Exception ex)
        {
          transaction.Rollback();

          return StatusCode(500, ex.Message);
        }

      }
    }

    // private IActionResult Reviced(Credit credit)
    // {
    //   using (var transaction = _context.Database.BeginTransaction())
    //   {
    //     Sale calculate = new Sale();
    //     CreditContract contract = new CreditContract();
    //     List<CreditContractItem> contractItems = new List<CreditContractItem>();
    //     Models.Booking booking = new Models.Booking();

    //     try
    //     {
    //       var reserve = credit.sale;
    //       calculate = credit.sale;
    //       contract = credit.creditContract;
    //       contractItems = credit.creditContactItem.ToList();
    //       booking = _context.Booking.FirstOrDefault(b => b.BookingId == contract.BookingId);

    //       // Calculate
    //       calculate.UpdateDate = DateTime.Now;
    //       _context.Sale.Update(calculate);
    //       _context.SaveChanges();

    //       // Contract
    //       contract.UpdateDate = DateTime.Now;
    //       contract.RefNo = GenerateReferenceContract(contract.ContractId);
    //       _context.CreditContract.Update(contract);
    //       _context.SaveChanges();

    //       // ContractItem
    //       foreach (var item in contractItems)
    //       {
    //         item.ContractId = contract.ContractId;
    //         item.RefNo = contract.RefNo;

    //         //item.PayPrice = item.Balance;
    //         //item.PayVatPrice = item.BalanceVatPrice;
    //         //item.PayNetPrice = item.BalanceNetPrice;
    //         // สถานะยังไม่ชำระ
    //         item.Status = 13;

    //         item.CreateBy = contract.CreateBy;
    //         item.CreateDate = DateTime.Now;
    //         item.UpdateBy = contract.UpdateBy;
    //         item.UpdateDate = DateTime.Now;
    //       }

    //       _context.CreditContractItem.AddRange(contractItems);
    //       _context.SaveChanges();

    //       // ค้นห่อผู้เช่าซื้อด้วยรหัส
    //       var __branch = _context.Branch.SingleOrDefault(x => x.BranchId == 1);

    //       var __company = _context.Company.FirstOrDefault(x => x.ComId == 1);

    //       // Booking
    //       if (booking.SellDate == null)
    //         booking.SellDate = DateTime.Now;

    //       booking.ReturnDepostit = reserve.ReturnDeposit;
    //       // กรณีมีการคืนเงินมัดจำ
    //       if (reserve.ReturnDeposit == 1 && booking.ReturnDepostit == 0)
    //       {
    //         booking.ReturnDepositPrice = reserve.ReturnDepositPrice;
    //         booking.ReturnDepNo = iSysParamService.GeerateeReturnDepositNo((int)contract.BranchId);
    //         booking.ReturnDepBy = contract.CreateBy;
    //         booking.ReturnDepDate = DateTime.Now;

    //       }
    //       else if (reserve.ReturnDeposit == 0)
    //       {
    //         booking.ReturnDepositPrice = 0;
    //       }

    //       booking.BookingStatus = 2; // สถานะขาย
    //       booking.PaymentPrice = calculate.DepositPrice;
    //       booking.PaymentType = booking.BookingDepositType;
    //       booking.CusSellName = __company.ComName;
    //       booking.CusTaxNo = __branch.BranchRegisterNo;
    //       booking.SellBy = contract.CreateBy;
    //       booking.LStartDate = calculate.FirstPayment.ToString("yyyy-MM-dd");
    //       booking.LPayDay = calculate.DueDate;
    //       booking.LTerm = calculate.InstalmentEnd;
    //       booking.LInterest = calculate.Interest;
    //       booking.LPriceTerm = calculate.InstalmentPrice;
    //       booking.ReturnDepositPrice = reserve.ReturnDeposit;

    //       // กรณีมีการคืนเงินมัดจำ
    //       if (reserve.ReturnDeposit == 1 && booking.ReturnDepostit == 0)
    //       {
    //         booking.ReturnDepositPrice = reserve.ReturnDepositPrice;
    //         booking.ReturnDepNo = iSysParamService.GeerateeReturnDepositNo((int)contract.BranchId);
    //         booking.ReturnDepBy = contract.CreateBy;
    //         booking.ReturnDepDate = DateTime.Now;
    //       }

    //       _context.Booking.Update(booking);
    //       _context.SaveChanges();

    //       transaction.Commit();

    //       var obj = new Dictionary<string, object>
    //                 {
    //                     {"contractId", contract.ContractId}
    //                 };

    //       return Ok(obj);

    //     }
    //     catch (Exception ex)
    //     {
    //       transaction.Rollback();
    //       return StatusCode(500, ex.Message);
    //     }
    //   }
    // }

    // private IActionResult Edited(Credit credit)
    // {
    //   using (var transaction = _context.Database.BeginTransaction())
    //   {
    //     Sale calculate = new Sale();
    //     CreditContract contract = new CreditContract();
    //     List<CreditContractItem> contractItems = new List<CreditContractItem>();

    //     try
    //     {
    //       calculate = credit.sale;
    //       contract = credit.creditContract;
    //       contractItems = credit.creditContactItem.ToList();

    //       // Calculate
    //       calculate.UpdateDate = DateTime.Now;
    //       _context.Update(calculate);
    //       _context.SaveChanges();

    //       // Contract
    //       contract.UpdateDate = DateTime.Now;
    //       _context.Update(contract);
    //       _context.SaveChanges();

    //       // ContractItem
    //       var contItem = _context.CreditContractItem.Where(o => o.ContractId == contract.ContractId).ToList();
    //       _context.RemoveRange(contItem);
    //       _context.SaveChanges();

    //       foreach (var item in contractItems)
    //       {
    //         item.ContractId = contract.ContractId;
    //         item.RefNo = contract.RefNo;

    //         //item.PayPrice = item.Balance;
    //         //item.PayVatPrice = item.BalanceVatPrice;
    //         //item.PayNetPrice = item.BalanceNetPrice;
    //         // สถานะยังไม่ชำระ
    //         item.Status = 13;

    //         item.CreateBy = contract.CreateBy;
    //         item.CreateDate = DateTime.Now;
    //         item.UpdateBy = contract.UpdateBy;
    //         item.UpdateDate = DateTime.Now;
    //       }

    //       _context.CreditContractItem.AddRange(contractItems);
    //       _context.SaveChanges();


    //       transaction.Commit();

    //       var obj = new Dictionary<string, object>
    //                 {
    //                     {"contractId", contract.ContractId}
    //                 };

    //       return Ok(obj);

    //     }
    //     catch (Exception ex)
    //     {
    //       transaction.Rollback();
    //       return StatusCode(500, ex.Message);
    //     }
    //   }
    // }

    private string GenerateReferenceContract(int? contractId)
    {
      if (contractId != null)
      {
        var rev = (from db in _context.CreditContract
                   where db.ContractId == contractId
                   select db.RefNo
                       ).FirstOrDefault();
        int runNumber = int.Parse(rev.Split(".")[1]) + 1;
        return rev = "REV." + runNumber.ToString("00");

      }
      else
      {
        return "REV.01";
      }
    }

    private bool SaleExists(int id)
    {
      return _context.Sale.Any(e => e.SaleId == id);
    }

    public class ModelEngine
    {
      public int LogId { get; set; }
      public string Model { get; set; }
      public string EngineNo { get; set; }
      public string FrameNo { get; set; }
    }

  }
}