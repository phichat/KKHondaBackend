using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KKHondaBackend.Controllers.Users
{
    [Route("api/Users")]
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
        [HttpGet("GetUserById")]
        public IActionResult GetUserById(int id)
        {

            var u = (from db in ctx.User
                     join b in ctx.Branch on db.BranchId equals b.BranchId into _user
                     from user in _user.DefaultIfEmpty()
                     where db.Id == id

                     select new
                     {
                         AdminName = $"{user.BranchName} ({user.BranchCode})",
                         Branch = db.BranchId,
                         db.Id,
                         Name = db.Username,
                         db.UserType,
                         FullName = db.Fullname
                     }).SingleOrDefault();

            return Ok(u);
        }

        public class UserDropdown
        {
            public int Id { get; set; }
            public string FullName { get; set; }
        }
    }
}
