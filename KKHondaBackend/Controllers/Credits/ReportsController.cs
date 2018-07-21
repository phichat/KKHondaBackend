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
    [Route("api/Credit/Reports")]
    public class ReportsController : Controller
    {
        private readonly dbwebContext _context;

        public ReportsController(dbwebContext context)
        {
            _context = context;
        }

        // GET: api/Reports
        [HttpGet]
        public IEnumerable<CreditContractItem> GetCreditContractItem()
        {
            return _context.CreditContractItem;
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCreditContractItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var creditContractItem = await _context.CreditContractItem.SingleOrDefaultAsync(m => m.ContractItemId == id);

            if (creditContractItem == null)
            {
                return NotFound();
            }

            return Ok(creditContractItem);
        }
        
    }
}