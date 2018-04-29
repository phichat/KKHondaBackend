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
    public class ProductsController : Controller
    {
        private readonly dbwebContext ctx;

        public ProductsController(dbwebContext context)
        {
            ctx = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult GetInitProduct()
        {

            var types = (from prop in ctx.ProductType
                         where prop.TypeStatus.Equals(1) && (prop.TypeId.Equals(1) || prop.TypeId.Equals(2) || prop.TypeId.Equals(3))
                         select new
                         {
                             TypeId = prop.TypeId,
                             TypeCode = prop.TypeCode,
                             TypeName = prop.TypeName
                         }).ToList();


            var categories = (from prop in ctx.ProductCategory
                              where prop.CatStatus.Equals(1) && (prop.CatCode.Equals("MB") || prop.CatCode.Equals("BB"))
                              select new
                              {
                                  CatId = prop.CatId,
                                  CatCode = prop.CatCode,
                                  CatName = prop.CatName
                              }).ToList();


            var brands = ctx.ProductBrand
                            .Where(p => p.BrandStatus.Equals(1))
                            .Select(a => new
                            {
                                BrandId = a.BrandId,
                                BrandCode = a.BrandCode,
                                BrandName = a.BrandName
                            }).ToList();


            var classes = ctx.ProductClass
                             .Where(p => p.ClassStatus.Equals(1))
                             .Select(p => new
                             {
                                 ClassId = p.ClassId,
                                 ClassCode = p.ClassCode,
                                 ClassName = p.ClassName
                             }).ToList();


            var models = ctx.ProductModel
                            .Where(prop => prop.ModelStatus.Equals(1))
                            .Select(prop => new
                            {
                                ModelId = prop.ModelId,
                                ModelName = prop.ModelName,
                                ModelCode = prop.ModelCode
                            }).ToList();


            var colors = ctx.ProductColor
                            .Where(prop => prop.ColorStatus.Equals(1))
                            .Select(prop => new
                            {
                                ColorId = prop.ColorId,
                                ColorCode = prop.ColorCode,
                                ColorName = prop.ColorName
                            }).ToList();


            var obj = new Dictionary<string, object> {
                {"types", types},
                {"brands", brands},
                {"classes", classes},
                {"colors", colors},
                {"models", models},
                {"categories", categories}
            };

            return Ok(obj);
        }

        [HttpGet("/Products", Name = "Types")]
        public IActionResult GetTypes()
        {

            var types = (from prop in ctx.ProductType
                         where prop.TypeStatus.Equals(1) && (prop.TypeId.Equals(1) || prop.TypeId.Equals(2) || prop.TypeId.Equals(3))
                         select new
                         {
                             TypeId = prop.TypeId,
                             TypeCode = prop.TypeCode,
                             TypeName = prop.TypeName
                         }).ToList();
            return Ok(types);
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
