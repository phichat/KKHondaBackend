using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KKHondaBackend.Controllers.Users
{
    [ApiController]
    [Produces("application/json")]
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
                            FullName = db.FullName
                        }).ToList();

            return user;
        }

        // GET api/values/5
        [HttpGet("GetUserById")]
        public IActionResult GetUserById(int id)
        {

            var u = (from db in ctx.User
                     join b in ctx.Branch on db.BranchId equals b.BranchId into _b
                     from brh in _b.DefaultIfEmpty()
                     where db.Id == id

                     select new UserResCookie
                     {
                         Id = db.Id,
                         AdminName = $"{brh.BranchName} ({brh.BranchCode})",
                         FullName = db.FullName,
                         UserType = db.UserType,
                         BranchId = db.BranchId,
                         Branch = db.BranchId,
                         BranchName = brh.BranchName,
                         Department = db.Department,
                         GId = db.GId,
                         Name = db.Username
                     }).SingleOrDefault();

            if (u == null) return NotFound();

            var groupPage = (from gp in ctx.GroupPagePermission
                             join pg in ctx.PageList on gp.PageId equals pg.PageId into _pg
                             from pgl in _pg.DefaultIfEmpty()

                             join g in ctx.GroupPage on 
                                new { gid = gp.GId } equals 
                                new { gid = (int?)g.GId } into _g
                             from grp in _g.DefaultIfEmpty()

                             where gp.GId == u.GId
                             select new GroupPagePermissionRes
                             {
                                 GPPId = gp != null ? gp.GPPId : default(int),
                                 GId = gp != null ? gp.GId : default(int?),
                                 GName =  grp != null ? grp.GName : null,
                                 PageId = gp != null ? gp.PageId : default(int?),
                                 PageName = pgl != null ? pgl.PageName : null
                             }).ToList();

            u.GroupPagePermission = groupPage;

            return Ok(u);
        }

        public class UserDropdown
        {
            public int Id { get; set; }
            public string FullName { get; set; }
        }
    }
}
