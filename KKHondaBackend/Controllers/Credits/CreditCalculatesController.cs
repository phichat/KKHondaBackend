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

namespace KKHondaBackend.Controllers.Credits
{
    [Produces("application/json")]
    [Route("api/Credit/Calculates")]
    public class CreditCalculatesController : Controller
    {
        private readonly dbwebContext _context;

        public CreditCalculatesController(dbwebContext context)
        {
            _context = context;
        }

        // GET: api/CreditCalculates
        [HttpGet]
        public IEnumerable<CreditCalculate> GetCreditCalculate()
        {
            return _context.CreditCalculate;
        }

        [HttpGet("IRR")]
        public static double GetIRR(double a1, double a2, double a3)
        {
            
            return a1;
        }

        // GET: api/CreditCalculates/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCreditCalculate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var creditCalculate = await _context.CreditCalculate.SingleOrDefaultAsync(m => m.CalculateId == id);

            if (creditCalculate == null)
            {
                return NotFound();
            }

            return Ok(creditCalculate);
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

        // POST: api/CreditCalculates
        [HttpPost]
        public IActionResult PostCreditCalculate([FromBody] Credit credit)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}


            CreditCalculate calculate = credit.creditCalculate;
            CreditContractItem[] contractItem = credit.creditContactItem;

            if (CreditCalculateExists(calculate.CalculateId)) {
                // update
                return Update(credit);

            } else {
                // insert
                return Insert(credit);
            }
        }

        private IActionResult Insert(Credit credit){

            using (var transaction = _context.Database.BeginTransaction())
            {

                try{
                    CreditCalculate calculate = credit.creditCalculate;
                    List<CreditContractItem> contractItems = credit.creditContactItem.ToList();

                    var createDate = DateTime.Now;
                    var createBy = calculate.CreateBy;

                    calculate.CreateDate = createDate;
                    _context.CreditCalculate.Add(calculate);
                    _context.SaveChanges();

                    var contractItem = credit.creditContactItem.FirstOrDefault();
                    var contractNo = GenerateContractCode(contractItem.ContractBranchId);

                    CreditContract contract = new CreditContract();
                    contract.BookingId = calculate.BookingId;
                    contract.CalculateId = calculate.CalculateId;
                    contract.ContractNo = contractNo;
                    contract.CreateBy = createBy;
                    contract.CreateDate = createDate;
                    _context.CreditContract.Add(contract);
                    _context.SaveChanges();


                    foreach (var item in contractItems)
                    {
                        item.ContractId = contract.ContractId;
                        item.CreateBy = createBy;
                        item.CreateDate = createDate;

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

        private IActionResult Update(Credit credit) {
            
            CreditCalculate calculate = credit.creditCalculate;
            CreditContractItem[] contractItem = credit.creditContactItem;

            return Ok(credit);
        }

        private string GenerateContractCode(int branchId){

            var contractNo = (from db in _context.CreditContract
                              orderby db.ContractNo descending
                              select db.ContractNo
                             ).FirstOrDefault();
            
            string year = (DateTime.Now.Year + 543).ToString().Substring(2,2);
            string month = (DateTime.Now.Month).ToString("00");

            if (contractNo == null) {
                contractNo = "CO" + branchId.ToString("00") + year + month + "/" + "0001";
            } else {
                int runNumber = int.Parse(contractNo.Split("/")[1]);
                contractNo = "CO" + branchId.ToString("00") + year + month + "/" + runNumber.ToString("0000");
            }
            
            return contractNo;
        }

        private bool CreditCalculateExists(int id)
        {
            return _context.CreditCalculate.Any(e => e.CalculateId == id);
        }

    }
}