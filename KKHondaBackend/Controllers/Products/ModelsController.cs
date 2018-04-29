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
        [HttpGet]
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
                return NotFound();

            return Ok(models);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

    }
}
