using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KKHondaBackend.Controllers.Credits
{
    [Route("api/Credit/Contract")]
    public class CreditContractController : Controller
    {

        private readonly dbwebContext ctx;
        private readonly IBookingServices iBookService;
        private readonly IUserServices iUserService;
        private readonly ICustomerServices iCustService;

        public CreditContractController(
            dbwebContext context, 
            IBookingServices ibookService,
            IUserServices iuserService,
            ICustomerServices icustService
        )
        {
            ctx = context;
            iBookService = ibookService;
            iUserService = iuserService;
            iCustService = icustService;
        }

        // GET api/values/5
        [HttpGet("GetById")]
        public IActionResult Get(int id)
        {

            var cont = ctx.CreditContract.Where(prop => prop.ContractId == id).SingleOrDefault();

            var contItem = ctx.CreditContractItem.Where(p => p.ContractId == id).ToList();

            var calcu = ctx.CreditCalculate.Where(p => p.CalculateId == cont.CalculateId).SingleOrDefault();

            var booking = iBookService.GetBookingById(cont.BookingId);

            var userDropdown = iUserService.GetAllUserDropdowns();

            var customerDropdown = iCustService.GetCustomerTop100Dropdowns();

            var obj = new Dictionary<string, object>
            {
                {"creditContract", cont},
                {"creditContractItem", contItem},
                {"creditCalculate", calcu},
                {"booking", booking},
                {"userDropdown", userDropdown},
                {"customerDropdown", customerDropdown}
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
