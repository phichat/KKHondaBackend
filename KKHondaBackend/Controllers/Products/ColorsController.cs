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
    public class ColorsController : Controller
    {
        private readonly dbwebContext ctx;

        public ColorsController(dbwebContext context)
        {
            ctx = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var colors = ctx.ProductColor
                           .Where(prop => prop.ColorStatus.Equals(1))
                           .Select(prop => new
                           {
                               ColorId = prop.ColorId,
                               ColorCode = prop.ColorCode,
                               ColorName = prop.ColorName
                           }).ToList();
            if (colors == null)
                return NoContent();

            return Ok(colors);
        }

        // GET api/values/5
        [HttpGet("FilterByKey")]
        public IActionResult FilterByKey(int modelId)
        {
            var colors = (from m in ctx.ProductColor
                          join p in ctx.Product on m.ColorId equals p.ColorId into a
                          from b in a.DefaultIfEmpty()
                          where b.ModelId.Equals(modelId)
                          select new
                          {
                              ColorId = b.ColorId,
                              ColorCode = m.ColorCode,
                              ColorName = m.ColorName
                          }).ToList();

            if (colors == null)
                return NoContent();

            return Ok(colors);
        }


    }
}
