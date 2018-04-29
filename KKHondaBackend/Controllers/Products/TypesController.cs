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
    [Route("api/Products/Types")]
    public class TypesController : Controller
    {
        private readonly dbwebContext ctx;

        public TypesController(dbwebContext context)
        {
            ctx = context;
        }


        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var types = (from prop in ctx.ProductType
                         where prop.TypeStatus.Equals(1) && (prop.TypeId.Equals(1) || prop.TypeId.Equals(2) || prop.TypeId.Equals(3))
                         select new
                         {
                             TypeId = prop.TypeId,
                             TypeCode = prop.TypeCode,
                             TypeName = prop.TypeName
                         }).ToList();

            if (types == null)
                return NotFound();

            return Ok(types);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

       
    }
}
