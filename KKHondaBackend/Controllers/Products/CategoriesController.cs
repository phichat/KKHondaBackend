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
    public class CategoriesController : Controller
    {
        private readonly dbwebContext ctx;

        public CategoriesController(dbwebContext context)
        {
            ctx = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var categories = (from prop in ctx.ProductCategory
                              where prop.CatStatus.Equals(1) && (prop.CatCode.Equals("MB") || prop.CatCode.Equals("BB"))
                              select new
                              {
                                  CatId = prop.CatId,
                                  CatCode = prop.CatCode,
                                  CatName = prop.CatName
                              }).ToList();
            if (categories == null)
                return NotFound();

            return Ok(categories);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

    }
}
