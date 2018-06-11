using System;
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

        public CreditCalculatesController(dbwebContext context, IBookingServices iBookingService)
        {
            _context = context;
            iBookService = iBookingService;
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

        [HttpPost("Create")] 
        public IActionResult Create([FromBody] Credit credit)
        {
            return Created(credit);
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
                    contract.ContractNo = GenerateContractCode((int)contract.BranchId);
                    contract.CreateDate = DateTime.Now;
                    _context.CreditContract.Add(contract);
                    _context.SaveChanges();

                    // ContractItem
                    foreach (var item in contractItems)
                    {
                        item.ContractId = contract.ContractId;
                        item.RefNo = contract.RefNo;
                        item.CreateBy = contract.CreateBy;
                        item.CreateDate = DateTime.Now;
                    }

                    _context.CreditContractItem.AddRange(contractItems);
                    _context.SaveChanges();
                   

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
                    contract.RefNo = GenerateReferenceContract(contract.ContractId);
                    _context.Update(contract);
                    _context.SaveChanges();

                    // ContractItem
                    foreach (var item in contractItems)
                    {
                        item.ContractId = contract.ContractId;
                        item.RefNo = contract.RefNo;
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

        private string GenerateContractCode(int branchId){

            var contractNo = (from db in _context.CreditContract
                              orderby db.ContractNo descending
                              where db.BranchId == branchId
                              select db.ContractNo
                             ).FirstOrDefault();
            
            string year = (DateTime.Now.Year + 543).ToString().Substring(2,2);
            string month = (DateTime.Now.Month).ToString("00");

            if (contractNo == null) {
                contractNo = "CO" + branchId.ToString("00") + year + month + "/" + "0001";
            } else {
                
                string preMonth = contractNo.Substring(6, 2);
                int runNumber = (preMonth == month) ? int.Parse(contractNo.Split("/")[1]) + 1 : 1;

                contractNo = "CO" + branchId.ToString("00") + year + month + "/" + runNumber.ToString("0000");
            }
            
            return contractNo;
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

    }
}