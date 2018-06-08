using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KKHondaBackend.Controllers.Users
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly dbwebContext ctx;

        public UserController(dbwebContext context)
        {
            ctx = context;
        }


        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }


        private IEnumerable<UserDropdown> GetForDropdown()
        {
            var user = (from db in ctx.User
                        select new UserDropdown
                        {
                            Id = db.Id,
                            FullName = db.Fullname
                        }).ToList();

            return user;
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

        public class UserDropdown
        {
            public int Id { get; set; }
            public string FullName { get; set; }
        }
    }
}
