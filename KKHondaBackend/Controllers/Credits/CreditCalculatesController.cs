using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult PostCreditCalculate([FromBody] Credit credits)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            try
            {
                var calculate = credits.creditCalculate;
                var contractItem = credits.creditContactItem;
            
                //var dateNow = DateTime.Now;
                //creditCalculate.CreateDate = dateNow;
                //_context.CreditCalculate.Add(creditCalculate);

                //CreditContract contract = new CreditContract();
                //contract.BookingId = creditCalculate.BookingId;
                //contract.CalculateId = creditCalculate.CalculateId;
                //contract.CreateBy = creditCalculate.CreateBy;
                //contract.CreateDate = dateNow;
                //_context.CreditContract.Add(contract);


                //creditContactItem.ContractId = contract.ContractId;
                //creditContactItem.CreateDate = dateNow;
                //_context.CreditContractItem.Add(creditContactItem);


                //await _context.SaveChangesAsync();

                return Ok();

            } catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }


            //return CreatedAtAction("GetCreditCalculate", new { id = creditCalculate.CalculateId }, creditCalculate);
        }

        private bool CreditCalculateExists(int id)
        {
            return _context.CreditCalculate.Any(e => e.CalculateId == id);
        }
    }
}