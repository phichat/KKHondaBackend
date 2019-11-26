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
            //var obj = new Dictionary<string, object> {
            //    {"types", types},
            //    {"brands", brands},
            //    {"classes", classes},
            //    {"colors", colors},
            //    {"models", models},
            //    {"categories", categories}
            //};

            return Ok();
        }


        [HttpGet("FilterByKey")]
        public IActionResult FilterByKey(int branchId, int typeId, int catId, int brandId, int modelId, int colorId)
        {
            var branch = (from p in ctx.Product
                            join s in ctx.StockReceive on p.ItemId equals s.ItemId into a
                            from b in a.DefaultIfEmpty()
                            join tl in ctx.TransferLog on b.LogId equals tl.LogId into a2
                            from c in a2.DefaultIfEmpty()

                            where b.BranchId.Equals(branchId) &&
                             b.ReceiveQty > 0 &&
                             p.TypeId.Equals(typeId) &&
                             p.CatId.Equals(catId) &&
                             p.BrandId.Equals(brandId) &&
                             p.ModelId.Equals(modelId) &&
                             p.ColorId.Equals(colorId)

                            select new
                            {
                                engineNo = c.EngineNo,
                                frameNo = c.FrameNo,
                                receiveQty = b.ReceiveQty,
                                balanceQty = b.BalanceQty
                            }).ToList();

            var product = (from p in ctx.Product
                           join s in ctx.StockReceive on p.ItemId equals s.ItemId into a
                           from b in a.DefaultIfEmpty()

                           where p.TypeId.Equals(typeId) &&
                             p.CatId.Equals(catId) &&
                             p.BrandId.Equals(brandId) &&
                             p.ModelId.Equals(modelId) &&
                             p.ColorId.Equals(colorId)

                           select new
                           {
                               itemId = p.ItemId,
                               typeId = p.TypeId,
                               catId = p.CatId,
                               modelId = p.ModelId,
                               colorId = p.ColorId,
                               branch = branch,
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
    }
}
