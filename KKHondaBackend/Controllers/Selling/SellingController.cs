using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;

namespace KKHondaBackend.Controllers.Selling
{
    [Produces("application/json")]
    [Route("api/Selling")]
    public class SellingController : Controller
    {
        private readonly dbwebContext ctx;

        public SellingController(dbwebContext context)
        {
            ctx = context;
        }

        // GET: api/Selling
        [HttpGet]
        public IActionResult GetAll()
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

        [HttpGet("Selling/Types")]
        public IActionResult GetTypes(){
            
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

        [HttpGet("/Categories")]
        public IActionResult GetCategories()
        {
            var categories = (from prop in ctx.ProductCategory
                              where prop.CatStatus.Equals(1) && (prop.CatCode.Equals("MB") || prop.CatCode.Equals("BB"))
                              select new
                              {
                                  CatId = prop.CatId,
                                  CatCode = prop.CatCode,
                                  CatName = prop.CatName
                              }).ToList();
            return Ok(categories);
        }

        [HttpGet("/Brands")]
        public IActionResult GetBrands()
        {
            var brands = ctx.ProductBrand
                      .Where(p => p.BrandStatus.Equals(1))
                      .Select(a => new
                      {
                          BrandId = a.BrandId,
                          BrandCode = a.BrandCode,
                          BrandName = a.BrandName
                      }).ToList();
            return Ok(brands);
        }

        [HttpGet("/Classes")]
        public IActionResult GetClasses()
        {
            var classes = ctx.ProductClass
                             .Where(p => p.ClassStatus.Equals(1))
                             .Select(p => new
                             {
                                 ClassId = p.ClassId,
                                 ClassCode = p.ClassCode,
                                 ClassName = p.ClassName
                             }).ToList();
            return Ok(classes);
        }

        [HttpGet("/Colors")]
        public IActionResult GetColors()
        {
            var colors = ctx.ProductColor
                            .Where(prop => prop.ColorStatus.Equals(1))
                            .Select(prop => new
                            {
                                ColorId = prop.ColorId,
                                ColorCode = prop.ColorCode,
                                ColorName = prop.ColorName
                            }).ToList();
            return Ok(colors);
        }

        [HttpGet("Models")]
        public IActionResult GetModels()
        {
            var models = ctx.ProductModel
                           .Where(prop => prop.ModelStatus.Equals(1))
                           .Select(prop => new
                           {
                               ModelId = prop.ModelId,
                               ModelName = prop.ModelName,
                               ModelCode = prop.ModelCode
                           }).ToList();
            return Ok(models);
        }

        // GET: api/Selling/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Selling
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Selling/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
