using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KKHondaBackend.Controllers.Selling
{
    [Route("api/Selling/[controller]")]
    public class SellActivityController : Controller
    {
        private readonly dbwebContext ctx;

        public SellActivityController(dbwebContext context)
        {
            ctx = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var sellActivity = (from s in ctx.SellActivity
                                join t in ctx.SellType on s.SellTypeId equals t.SellTypeId into a
                                from b in a.DefaultIfEmpty()
                                where s.ActiveStatus.Equals(true)
                                select new
                                {
                                    activityId = s.ActivityId,
                                    activityCode = s.ActivityCode,
                                    activityName = s.ActivityName,
                                    sellTypeId = b.SellTypeId,
                                    sellTypeCode = b.SellTypeCode,
                                    sellTypeName = b.SellTypeName
                                }).ToList();

            if (sellActivity == null)
                return NoContent();

            return Ok(sellActivity);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
