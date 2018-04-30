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
        public IActionResult FilterByKey(int typeId, int catId, int brandId, int modelId, int colorId)
        {
            var quantity = (from p in ctx.Product
                            join t in ctx.TransferLog on p.ItemId equals t.ItemId into a
                            from b in a.DefaultIfEmpty()
                            where p.TypeId.Equals(typeId) && p.CatId.Equals(catId) && p.BrandId.Equals(brandId) &&
                            b.ModelId.Equals(modelId) && b.ColorId.Equals(colorId)
                            select new
                            {
                                engineNo = b.EngineNo,
                                frameNo = b.FrameNo,
                                qty = b.Qty,
                                bQty = b.BQty
                            }).ToList();

            var product = (from p in ctx.Product
                           join t in ctx.TransferLog on p.ItemId equals t.ItemId into a
                           from b in a.DefaultIfEmpty()
                           where p.TypeId.Equals(typeId) && p.CatId.Equals(catId) && p.BrandId.Equals(brandId) &&
                            p.ModelId.Equals(modelId) && p.ColorId.Equals(colorId)
                           select new
                           {
                               typeId = p.TypeId,
                               catId = p.CatId,
                               brandId = p.BrandId,
                               modelId = p.ModelId,
                               colorId = p.ColorId,
                               quantity = quantity,
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
