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
    [ApiController]
    [Produces("application/json")]
    [Route("api/Products/[controller]")]
    public class BrandsController : Controller
    {

        private readonly dbwebContext ctx;

        public BrandsController(dbwebContext context)
        {
            ctx = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var brands = ctx.ProductBrand
                           .Where(p => p.BrandStatus.Equals(1))
                           .Select(a => new
                           {
                               BrandId = a.BrandId,
                               BrandCode = a.BrandCode,
                               BrandName = a.BrandName
                           }).ToList();

            if (brands == null)
                return NotFound();

            return Ok(brands);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

       
    }
}
