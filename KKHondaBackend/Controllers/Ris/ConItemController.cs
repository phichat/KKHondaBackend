using KKHondaBackend.Data;
using KKHondaBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using KKHondaBackend.Services.Ris;

namespace KKHondaBackend.Controllers.Ris
{
    [Route("api/Ris/[controller]")]
    public class ConItemController : Controller
    {
        private readonly dbwebContext ctx;
        private readonly IMSendbackService iMSendback;

        public ConItemController(
            dbwebContext _ctx,
            IMSendbackService _iMSendback
            )
        {
            ctx = _ctx;
            iMSendback = _iMSendback;
        }

        public IEnumerable<CarRegisListItemRes> List
        {
            get => (from list in ctx.CarRegisListItem
                    join item in ctx.CarRegisList on list.BookingId equals item.BookingId into a
                    from _n in a.DefaultIfEmpty()
                    select new CarRegisListItemRes
                    {
                        RunId = list.RunId,
                        BookingId = list.BookingId,
                        BookingNo = _n.BookingNo,
                        ItemCode = list.ItemCode,
                        ItemName = list.ItemName,
                        ItemPrice1 = list.ItemPrice1,
                        ItemVatPrice1 = list.ItemVatPrice1,
                        ItemCutBalance = list.ItemCutBalance,
                        ItemPrice2 = list.ItemPrice2,
                        ItemPriceTotal = list.ItemPriceTotal,
                        State = list.State,
                        DateReceipt = list.DateReceipt,
                        Remark = list.Remark
                    }
             );
        }

        [HttpGet("GetByConNo")]
        public IActionResult GetByConNo(string conNo)
        {
            var list = List.Where(x => x.BookingNo == conNo);
            var listItemDoc = new List<CarRegisListItemDocRes>();
            var itemSendBack = new CarRegisMSendback();
            CarRegisListItemDocRes itemDoc;

            foreach (var item in list)
            {
                switch (item.ItemCode)
                {
                    case "EXP10001": //จดทะเบียนรถใหม่
                        itemSendBack = iMSendback.Active.FirstOrDefault(x => x.NewCar == true);
                        break;

                    case "EXP10002": //ต่อทะเบียน
                    // case "EXP10004": //ต่อภาษี
                        itemSendBack = iMSendback.Active.FirstOrDefault(x => x.Tag == true);
                        break;

                    case "EXP10003": //ซื้อ พ.ร.บ
                        itemSendBack = iMSendback.Active.FirstOrDefault(x => x.Act == true);
                        break;

                    case "EXP10004": //ประกันภัย
                        itemSendBack = iMSendback.Active.FirstOrDefault(x => x.Warranty == true);
                        break;
                }

                if (itemSendBack != null && itemSendBack.Code != null)
                {
                    itemDoc = new CarRegisListItemDocRes
                    {
                        BookingNo = conNo,
                        SendBackCode = itemSendBack.Code,
                        SendBackName = itemSendBack.Name
                    };
                    listItemDoc.Add(itemDoc);
                };
            }

            var mSendBackOther = iMSendback.Active.Where(x => x.Other == true);
            foreach (var item in mSendBackOther)
            {
                var checkItem = listItemDoc.Any(x => item.Code.Contains(x.SendBackCode));
                if (!checkItem)
                {
                    itemDoc = new CarRegisListItemDocRes
                    {
                        BookingNo = conNo,
                        SendBackCode = item.Code,
                        SendBackName = item.Name
                    };
                    listItemDoc.Add(itemDoc);
                }
            }

            var res = new CarRegisItemRes
            {
                CarRegisListItemRes = list.ToList(),
                CarRegisListItemDocRes = listItemDoc.ToList()
            };

            return Ok(res);
        }
    }
}