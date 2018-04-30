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
    [Route("api/Products/[controller]")]
    public class ModelsController : Controller
    {

        private readonly dbwebContext ctx;

        public ModelsController(dbwebContext context)
        {
            ctx = context;
        }

        // GET: api/values
        [HttpGet("")]
        public IActionResult Get()
        {

            var models = ctx.ProductModel
                            .Where(prop => prop.ModelStatus.Equals(1))
                            .Select(prop => new
                            {
                                ModelId = prop.ModelId,
                                ModelName = prop.ModelName,
                                ModelCode = prop.ModelCode
                            }).ToList();
            if (models == null)
                return NoContent();

            return Ok(models);
        }
        
        [HttpGet("FilterByKey")]
        public IActionResult FilterByKey(int typeId, int catId, int brandId)
        {
            var models = (from m in ctx.ProductModel
                          join p in ctx.Product on m.ModelId equals p.ModelId into a
                          from b in a.DefaultIfEmpty()
                          where b.TypeId == typeId && b.CatId == catId && b.BrandId == brandId
                          select new
                          {
                              ModelId = b.ModelId,
                              ModelName = m.ModelName,
                              ModelCode = m.ModelCode
                          }).ToList();

            if (models == null)
                return NoContent();
            
            return Ok(models);
        }

    }
}
