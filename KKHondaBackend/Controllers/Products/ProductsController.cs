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
        public IActionResult FilterByKey(int branchId, int typeId, int catId, int brandId, int modelId, int colorId)
        {
            //var quantity = (from p in ctx.Product
                            //join s in ctx.StockReceive on p.ItemId equals s.ItemId into a1
                            //from a2 in a1.DefaultIfEmpty()
                            //join t in ctx.TransferLog on a2.LogId equals t.LogId into a3
                            //from a4 in a3.DefaultIfEmpty()

                            //where a2.BranchId.Equals(branchId) &&
                            //p.TypeId.Equals(typeId) && p.CatId.Equals(catId) && 
                            //p.BrandId.Equals(brandId) && p.ModelId.Equals(modelId) && 
                            //p.ColorId.Equals(colorId)

                            //group a4 by new
                            //{
                            //    a4.EngineNo,
                            //    a4.FrameNo
                            //} into g

                            //select new
                            //{
                            //    engineNo = ,
                            //    frameNo = g.FrameNo,
                            //    qty = g.Sum(x => x.),
                            //    bQty = b.BQty
                            //}).ToList();

            //var product = (from p in ctx.Product
            //               join s in ctx.StockReceive on p.ItemId equals s.ItemId into a
            //               from b in a.DefaultIfEmpty()
            //               where b.BranchId.Equals(branchId) &&
            //                p.TypeId.Equals(typeId) && p.CatId.Equals(catId) && 
            //                p.BrandId.Equals(brandId) && p.ModelId.Equals(modelId) && 
            //                p.ColorId.Equals(colorId)
            //               select new
            //               {
            //                   typeId = p.TypeId,
            //                   catId = p.CatId,
            //                   brandId = p.BrandId,
            //                   modelId = p.ModelId,
            //                   colorId = p.ColorId,
            //                   quantity = quantity,
            //                   sellPrice = p.SellPrice,
            //                   sellPrice2 = p.SellPrice,
            //                   sellVatPrice = p.SellVatPrice,
            //                   sellVat = p.SellVat,
            //                   discountPrice = 0,
            //                   discountVat = 0,
            //                   sellNet = p.SellNet
            //               }).ToList();

            //if (product == null)
                //return NoContent();

            return Ok();
        }
    }
}
