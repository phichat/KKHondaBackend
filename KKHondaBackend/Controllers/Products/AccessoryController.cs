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
    [Route("api/Products/Accessory")]
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
            var product = (from sr in ctx.StockReceive
                           group sr by new
                           {
                               sr.ItemId,
                               sr.BranchId
                           } into a1
                           select new
                           {
                               itemId = a1.Key.ItemId,
                               branchId = a1.Key.BranchId,
                               rQty = a1.Sum(x => x.ReceiveQty),
                               bQty = a1.Sum(x => x.BalanceQty)

                           } into a2
                           join pro in ctx.Product on a2.itemId equals pro.ItemId into a3
                           from a4 in a3.DefaultIfEmpty()
                           where a2.branchId.Equals(branchId) &&
                           a4.ItemStatus.Equals(1) &&
                           a4.CatId.Equals(8) &&
                           a4.PartCode.Contains(term) ||
                           a4.PartName.Contains(term) ||
                           a4.PartClass.Contains(term)
                           select new
                           {
                               itemId = a4.ItemId,
                               typeId = a4.TypeId,
                               catId = a4.CatId,
                               partCode = a4.PartCode,
                               partName = a4.PartName,
                               partClass = a4.PartClass,
                               partSource = a4.PartSource,
                               receiveQty = a2.rQty,
                               balanceQty = a2.bQty,
                               sellPrice = a4.SellPrice,
                               sellPrice2 = a4.SellPrice,
                               sellVatPrice = a4.SellVatPrice,
                               sellVat = a4.SellVat,
                               discountPrice = 0.00,
                               discountVat = 0.00,
                               sellNet = a4.SellNet
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
