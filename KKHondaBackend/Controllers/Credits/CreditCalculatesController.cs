﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using KKHondaBackend.Services;

namespace KKHondaBackend.Controllers.Credits
{
    [Produces("application/json")]
    [Route("api/Credit/Calculates")]
    public class CreditCalculatesController : Controller
    {
        private readonly dbwebContext _context;
        private readonly IBookingServices iBookService;
        private readonly ISysParameterService iSysParamService;
        private readonly ICustomerServices iCustomerService;

        public CreditCalculatesController(
            dbwebContext context, 
            IBookingServices iBookingService,
            ISysParameterService isysParamService,
            ICustomerServices icustService
        )
        {
            _context = context;
            iBookService = iBookingService;
            iSysParamService = isysParamService;
            iCustomerService = icustService;
        }

        // GET: api/CreditCalculates
        [HttpGet]
        public IEnumerable<CreditCalculate> GetCreditCalculate()
        {
            return _context.CreditCalculate;
        }

        // GET: api/CreditCalculates/5
        [HttpGet("GetById")]
        public IActionResult GetCreditCalculate(int calculateId)
        {
            
            var calc = _context.CreditCalculate
                               .Where(p => p.CalculateId == calculateId)
                               .SingleOrDefault();

            var cont = _context.CreditContract
                               .Where(p => p.CalculateId == calculateId)
                               .SingleOrDefault();

            var contItem = _context.CreditContractItem
                                   .Where(p => p.ContractId == cont.ContractId && p.RefNo == cont.RefNo)
                                   .OrderBy(o => o.InstalmentNo)
                                   .ToList();

            var book = iBookService.GetBookingById(calc.BookingId);

            var obj = new Dictionary<string, object>
            {
                {"creditCalculate", calc},
                {"creditContract", cont},
                {"creditContractItem", contItem},
                {"booking", book}
            };

            return Ok(obj);
        }

        // PUT: api/CreditCalculates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreditCalculate([FromRoute] int id, [FromBody] CreditCalculate creditCalculate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != creditCalculate.CalculateId)
            {
                return BadRequest();
            }

            _context.Entry(creditCalculate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditCalculateExists(id))
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
        public IActionResult GetEngineByKeyword(int bookingId, int branchId, string term) {

            try {

                var bookItem = _context.BookingItem.SingleOrDefault(p => p.BookingId == bookingId && p.ItemDetailType == 1);

                var model = _context.ProductModel.SingleOrDefault(p => p.ModelId == bookItem.ModelId);

                var stockReceive = (from db in _context.StockReceive

                                    join _log in _context.TransferLog on db.LogId equals _log.LogId into a1
                                    from log in a1.DefaultIfEmpty()

                                    orderby log.EngineNo ascending

                                    where log.ItemId == bookItem.ItemId &&
                                    log.ModelId == bookItem.ModelId &&
                                    log.ColorId == bookItem.ColorId &&
                                    log.ReceiverId == branchId &&   
                                    db.BranchId == branchId &&
                                    db.BalanceQty > 0 &&
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

        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] Credit credit)
        {
            return Edited(credit);
        }

        [HttpPost("Revice")]
        public IActionResult Revice([FromBody] Credit credit)
        {
            return Reviced(credit);
        }

        private IActionResult Created(Credit credit){

            using (var transaction = _context.Database.BeginTransaction())
            {
                CreditCalculate calculate = new CreditCalculate();
                CreditContract contract = new CreditContract();
                List<CreditContractItem> contractItems = new List<CreditContractItem>();

                try{
                    calculate = credit.creditCalculate;
                    contract = credit.creditContract;
                    contractItems = credit.creditContactItem.ToList();

                    // Calculate
                    calculate.CreateDate = DateTime.Now;
                    _context.CreditCalculate.Add(calculate);
                    _context.SaveChanges();

                    // Contract
                    contract.CalculateId = calculate.CalculateId;
                    contract.RefNo = GenerateReferenceContract(null);
                    contract.ContractNo = iSysParamService.GenerateContractNo((int)contract.BranchId);
                    contract.CreateDate = DateTime.Now;
                    _context.CreditContract.Add(contract);
                    _context.SaveChanges();

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

                    // หลังจากเลือกรายการที่ค้นหาแล้วให้เอา log_id ที่ได้มาเก็บตอนทำการขาย      
                    // มีเงื่อนไขโดย itemDetailType==1(สินค้าประเภทรถ)
                    var bookItem = _context.BookingItem.FirstOrDefault(x => x.BookingId == contract.BookingId && x.ItemDetailType == 1);
                    if (bookItem != null) {
                        bookItem.LogReceiveId = calculate.LogReceiveId;
                        _context.BookingItem.Update(bookItem);
                        _context.SaveChanges();
                    }

                    var stockReceive = _context.StockReceive.FirstOrDefault(x => x.LogId == calculate.LogReceiveId);
                    if(stockReceive != null) {
                        stockReceive.BalanceQty = stockReceive.BalanceQty - 1;
                        _context.StockReceive.Update(stockReceive);
                        _context.SaveChanges();

                    }

                    transaction.Commit();

                    var obj = new Dictionary<string, object>
                    {
                        {"contractId", contract.ContractId}
                    };

                    return Ok(obj);

                } catch(Exception ex){
                    transaction.Rollback();

                    return StatusCode(500, ex.Message);
                }

            }
         }

        private IActionResult Reviced(Credit credit)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                CreditCalculate calculate = new CreditCalculate();
                CreditContract contract = new CreditContract();
                List<CreditContractItem> contractItems = new List<CreditContractItem>();
                Models.Booking booking = new Models.Booking();

                try 
                {
                    calculate = credit.creditCalculate;
                    contract = credit.creditContract;
                    contractItems = credit.creditContactItem.ToList();
                    booking = _context.Booking.FirstOrDefault(b => b.BookingId == contract.BookingId);

                    // Calculate
                    calculate.UpdateDate = DateTime.Now;
                    _context.CreditCalculate.Update(calculate);
                    _context.SaveChanges();

                    // Contract
                    contract.UpdateDate = DateTime.Now;
                    contract.RefNo = GenerateReferenceContract(contract.ContractId);
                    _context.CreditContract.Update(contract);
                    _context.SaveChanges();

                    // ContractItem
                    foreach (var item in contractItems)
                    {
                        item.ContractId = contract.ContractId;
                        item.RefNo = contract.RefNo;

                        //item.PayPrice = item.Balance;
                        //item.PayVatPrice = item.BalanceVatPrice;
                        //item.PayNetPrice = item.BalanceNetPrice;
                        // สถานะยังไม่ชำระ
                        item.Status = 13;

                        item.CreateBy = contract.CreateBy;
                        item.CreateDate = DateTime.Now;
                        item.UpdateBy = contract.UpdateBy;
                        item.UpdateDate = DateTime.Now;
                    }

                    _context.CreditContractItem.AddRange(contractItems);
                    _context.SaveChanges();

                    // ค้นห่อผู้เช่าซื้อด้วยรหัส
                    var __branch = _context.Branch.SingleOrDefault(x => x.BranchId == 1);

                    var __company = _context.Company.FirstOrDefault(x => x.ComId == 1);

                    // Booking
                    if (booking.SellDate == null) 
                        booking.SellDate = DateTime.Now;

                    booking.ReturnDepostit = calculate.ReturnDeposit;
                    // กรณีมีการคืนเงินมัดจำ
                    if (calculate.ReturnDeposit == 1 && booking.ReturnDepostit == 0)
                    {
                        booking.ReturnDepositPrice = calculate.ReturnDepositPrice;
                        booking.ReturnDepNo = iSysParamService.GeerateeReturnDepositNo((int)contract.BranchId);
                        booking.ReturnDepBy = contract.CreateBy;
                        booking.ReturnDepDate = DateTime.Now;

                    } else if (calculate.ReturnDeposit == 0)
                    {
                        booking.ReturnDepositPrice = 0;
                    }

                    booking.BookingStatus = 2; // สถานะขาย
                    booking.PaymentPrice = calculate.DepositPrice;
                    booking.PaymentType = booking.BookingDepositType;
                    booking.CusSellName = __company.ComName;
                    booking.CusTaxNo = __branch.BranchRegisterNo;
                    booking.SellBy = contract.CreateBy;
                    booking.LStartDate = calculate.FirstPayment.ToString("yyyy-MM-dd");
                    booking.LPayDay = calculate.DueDate;
                    booking.LTerm = calculate.InstalmentEnd;
                    booking.LInterest = calculate.Interest;
                    booking.LPriceTerm = calculate.InstalmentPrice;
                    booking.ReturnDepositPrice = calculate.ReturnDeposit;

                    // กรณีมีการคืนเงินมัดจำ
                    if (calculate.ReturnDeposit == 1 && booking.ReturnDepostit == 0)
                    {
                        booking.ReturnDepositPrice = calculate.ReturnDepositPrice;
                        booking.ReturnDepNo = iSysParamService.GeerateeReturnDepositNo((int)contract.BranchId);
                        booking.ReturnDepBy = contract.CreateBy;
                        booking.ReturnDepDate = DateTime.Now;
                    }

                    _context.Booking.Update(booking);
                    _context.SaveChanges();

                    transaction.Commit();

                    var obj = new Dictionary<string, object>
                    {
                        {"contractId", contract.ContractId}
                    };

                    return Ok(obj);

                } catch(Exception ex) {
                    transaction.Rollback();
                    return StatusCode(500, ex.Message);
                }
            }
        }

        private IActionResult Edited(Credit credit) {

            using (var transaction = _context.Database.BeginTransaction())
            {
                CreditCalculate calculate = new CreditCalculate();
                CreditContract contract = new CreditContract();
                List<CreditContractItem> contractItems = new List<CreditContractItem>();

                try
                {
                    calculate = credit.creditCalculate;
                    contract = credit.creditContract;
                    contractItems = credit.creditContactItem.ToList();

                    // Calculate
                    calculate.UpdateDate = DateTime.Now;
                    _context.Update(calculate);
                    _context.SaveChanges();

                    // Contract
                    contract.UpdateDate = DateTime.Now;
                    _context.Update(contract);
                    _context.SaveChanges();

                    // ContractItem
                    var contItem = _context.CreditContractItem.Where(o => o.ContractId == contract.ContractId).ToList();
                    _context.RemoveRange(contItem);
                    _context.SaveChanges();

                    foreach (var item in contractItems)
                    {
                        item.ContractId = contract.ContractId;
                        item.RefNo = contract.RefNo;

                        //item.PayPrice = item.Balance;
                        //item.PayVatPrice = item.BalanceVatPrice;
                        //item.PayNetPrice = item.BalanceNetPrice;
                        // สถานะยังไม่ชำระ
                        item.Status = 13;

                        item.CreateBy = contract.CreateBy;
                        item.CreateDate = DateTime.Now;
                        item.UpdateBy = contract.UpdateBy;
                        item.UpdateDate = DateTime.Now;
                    }

                    _context.CreditContractItem.AddRange(contractItems);
                    _context.SaveChanges();


                    transaction.Commit();

                    var obj = new Dictionary<string, object>
                    {
                        {"contractId", contract.ContractId}
                    };

                    return Ok(obj);
                    
                } catch (Exception ex){
                    transaction.Rollback();
                    return StatusCode(500, ex.Message);
                }
            }

        }


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

            } else {
                return "REV.01";
            }
        }

        private bool CreditCalculateExists(int id)
        {
            return _context.CreditCalculate.Any(e => e.CalculateId == id);
        }

        public class ModelEngine {
            public int LogId { get; set; }
            public string Model { get; set; }
            public string EngineNo { get; set; }    
            public string FrameNo { get; set; }
        }

    }
}