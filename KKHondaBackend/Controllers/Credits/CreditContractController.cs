using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KKHondaBackend.Controllers.Credits
{
    [Route("api/[controller]")]
    public class CreditContractController : Controller
    {
        
        private readonly dbwebContext ctx;

        public CreditContractController(dbwebContext context)
        {
            ctx = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
           
            var cont = ctx.CreditContract.Where(prop => prop.ContractId == id).SingleOrDefault();

            var calcu = ctx.CreditCalculate.Where(p => p.CalculateId == cont.CalculateId).SingleOrDefault();

            var booking = ctx.Booking.Where(p => p.BookingId == cont.BookingId).SingleOrDefault();

            var cust = ctx.MCustomer.Where(p => p.CustomerCode == booking.CustomerCode).SingleOrDefault();

            var bookingItem = ctx.BookingItem.Where(prop => prop.BookingId == cont.BookingId).ToList();

            var contItem = ctx.CreditContractItem.Where(p => p.ContractId == id).ToList();

            var obj = new Dictionary<string, object>
            {
                {"creditContract", cont},
                {"creditContractItem", contItem},
                {"creditCalculate", calcu},
                {"booking", booking},
                {"bookingItem", bookingItem},
                {"customer", cust}
            };


            return Ok(obj);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
