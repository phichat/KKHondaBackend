using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KKHondaBackend.Controllers.Products
{
    [Produces("application/json")]
    [Route("api/Products/[controller]")]
    public class ClassesController : Controller
    {

        private readonly dbwebContext ctx;

        public ClassesController(dbwebContext context)
        {
            ctx = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var classes = ctx.ProductClass
                             .Where(p => p.ClassStatus.Equals(1))
                             .Select(p => new
                             {
                                 ClassId = p.ClassId,
                                 ClassCode = p.ClassCode,
                                 ClassName = p.ClassName
                             }).ToList();
            if (classes == null)
                return NotFound();

            return Ok(classes);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
