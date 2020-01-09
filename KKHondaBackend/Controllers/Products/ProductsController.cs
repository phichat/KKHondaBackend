using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KKHondaBackend.Controllers.Products
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
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

        [HttpGet("[Action]")]
        public async Task<IActionResult> GetMCByKeyword(string term) {
            var list = (from p in ctx.Product
            join c in ctx.ProductCategory on p.CatId equals c.CatId
            join b in ctx.ProductBrand on p.BrandId equals b.BrandId
            join t in ctx.ProductType on p.TypeId equals t.TypeId
            join co in ctx.ProductColor on p.ColorId equals co.ColorId
            join m in ctx.ProductModel on p.ModelId equals m.ModelId
            where p.CatId == 4 && p.ItemStatus == 1 &&
            ($"{c.CatName} {t.TypeName} {b.BrandName} {m.ModelName} {co.ColorName}")
            .ToLower()
            .Contains(term.ToLower())
            select new {
                p.ItemId,
                c.CatName,
                t.TypeName,
                b.BrandName,
                m.ModelName,
                co.ColorName,
                p.SellPrice,
                p.SellVat,
                p.SellVatPrice,
                p.SellNet
            });
            return Ok(await list.ToListAsync());
        }

        // [HttpGet("FilterByKey")]
        // public IActionResult FilterByKey(int branchId, int typeId, int catId, int brandId, int modelId, int colorId)
        // {
        //     var branch = (from p in ctx.Product
        //                     join s in ctx.StockReceive on p.ItemId equals s.ItemId into a
        //                     from b in a.DefaultIfEmpty()
        //                     join tl in ctx.TransferLog on b.LogId equals tl.LogId into a2
        //                     from c in a2.DefaultIfEmpty()

        //                     where b.BranchId.Equals(branchId) &&
        //                      b.ReceiveQty > 0 &&
        //                      p.TypeId.Equals(typeId) &&
        //                      p.CatId.Equals(catId) &&
        //                      p.BrandId.Equals(brandId) &&
        //                      p.ModelId.Equals(modelId) &&
        //                      p.ColorId.Equals(colorId)

        //                     select new
        //                     {
        //                         engineNo = c.EngineNo,
        //                         frameNo = c.FrameNo,
        //                         receiveQty = b.ReceiveQty,
        //                         balanceQty = b.BalanceQty
        //                     }).ToList();

        //     var product = (from p in ctx.Product
        //                    join s in ctx.StockReceive on p.ItemId equals s.ItemId into a
        //                    from b in a.DefaultIfEmpty()

        //                    where p.TypeId.Equals(typeId) &&
        //                      p.CatId.Equals(catId) &&
        //                      p.BrandId.Equals(brandId) &&
        //                      p.ModelId.Equals(modelId) &&
        //                      p.ColorId.Equals(colorId)

        //                    select new
        //                    {
        //                        itemId = p.ItemId,
        //                        typeId = p.TypeId,
        //                        catId = p.CatId,
        //                        modelId = p.ModelId,
        //                        colorId = p.ColorId,
        //                        branch = branch,
        //                        sellPrice = p.SellPrice,
        //                        sellPrice2 = p.SellPrice,
        //                        sellVatPrice = p.SellVatPrice,
        //                        sellVat = p.SellVat,
        //                        discountPrice = 0,
        //                        discountVat = 0,
        //                        sellNet = p.SellNet
        //                    }).ToList();

        //     if (product == null)
        //         return NoContent();

        //     return Ok(product);
        // }
    }
}
