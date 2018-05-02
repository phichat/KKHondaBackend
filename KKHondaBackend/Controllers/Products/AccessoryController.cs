using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;

namespace KKHondaBackend.Controllers.Products
{
    [Produces("application/json")]
    [Route("api/Accessory")]
    public class AccessoryController : Controller
    {
        private readonly dbwebContext ctx;

        public AccessoryController(dbwebContext context)
        {
            ctx = context;
        }

        // GET: api/Accessory
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Accessory/5
        [HttpGet("FilterByKey")]
        public IActionResult FilterByKey(string term, int branchId)
        {
            //var quantity = (from p in ctx.Product
            //                join t in ctx.TransferLog on p.ItemId equals t.ItemId into a
            //                from b in a.DefaultIfEmpty()
            //                where p.PartCode.Contains(term) || p.PartName.Contains(term)
            //                select new
            //                {
            //                    engineNo = b.EngineNo,
            //                    frameNo = b.FrameNo,
            //                    qty = b.Qty,
            //                    bQty = b.BQty
            //                }).ToList();

            var product = (from p in ctx.Product
                           join t in ctx.TransferLog on p.ItemId equals t.ItemId into a
                           from b in a.DefaultIfEmpty()
                           where p.PartCode.Contains(term) || p.PartName.Contains(term)
                           select new
                           {
                               itemId = p.ItemId,
                               typeId = p.TypeId,
                               catId = p.CatId,
                               

                               //quantity = quantity,
                               sellPrice = p.SellPrice,
                               sellPrice2 = p.SellPrice,
                               sellVatPrice = p.SellVatPrice,
                               sellVat = p.SellVat,
                               discountPrice = 0,
                               discountVat = 0,
                               sellNet = p.SellNet
                           }).ToList();

            if (product == null)
                return NoContent();

            return Ok(product);
        }
        
        // POST: api/Accessory
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Accessory/5
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
