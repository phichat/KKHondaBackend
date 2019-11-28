using KKHondaBackend.Data;
using KKHondaBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using KKHondaBackend.Services.Ris;
using KKHondaBackend.Entities;

namespace KKHondaBackend.Controllers.Ris
{
  // [ApiController]
  [Produces("application/json")]
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

    private IEnumerable<CarRegisListItemRes> List
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
                ItemNetPrice1 = list.ItemNetPrice1,
                ItemCutBalance = list.ItemCutBalance,
                ItemPrice2 = list.ItemPrice2,
                ItemPrice3 = list.ItemPrice3,
                ItemPriceTotal = list.ItemPriceTotal,
                ItemTag = list.ItemTag,
                ItemType = list.ItemType,
                State = list.State,
                DateReceipt = list.DateReceipt,
                Remark = list.Remark
              }
       ).AsNoTracking();
    }

    [HttpGet("GetByConNo")]
    public IActionResult GetByConNo(string conNo)
    {
      var expensesTag = new List<string> {
                ExpensesTag.EXP10001,
                ExpensesTag.EXP10002
            };
      var expensesType = new List<int> {
        ExpensesType.Service,
        ExpensesType.Expenses
      };
      var list = List.Where(x =>
          x.BookingNo == conNo &&
          expensesTag.Contains(x.ItemTag) &&
          expensesType.Contains((int)x.ItemType)
          );
      var listItemDoc = new List<CarRegisListItemDocRes>();
      var itemSendBack = new CarRegisMSendback();
      CarRegisListItemDocRes itemDoc;

      foreach (var item in list)
      {
        switch (item.ItemCode)
        {
          case ExpensesTag.EXP10001: //จดทะเบียนรถใหม่
            itemSendBack = iMSendback.Active.FirstOrDefault(x => x.NewCar == true);
            break;

          case ExpensesTag.EXP10002: //ต่อทะเบียน
            itemSendBack = iMSendback.Active.FirstOrDefault(x => x.Tag == true);
            break;
        }



        if (itemSendBack != null && itemSendBack.Code != null)
        {
          if (listItemDoc.Any(o => o.SendBackCode == itemSendBack.Code))
            break;

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

      listItemDoc = (from item in listItemDoc
                     join d in ctx.CarRegisListItemDoc
                          on new { item.BookingNo, item.SendBackCode }
                          equals new { d.BookingNo, d.SendBackCode }
                          into _d
                     from doc in _d.DefaultIfEmpty()
                     join r in ctx.User on doc?.ReceiveBy equals r.Id into _r
                     join s in ctx.User on doc?.SendBy equals s.Id into _s
                     from rec in _r.DefaultIfEmpty()
                     from sed in _s.DefaultIfEmpty()
                     where item.BookingNo == conNo
                     select new CarRegisListItemDocRes
                     {
                       DocId = doc != null ? doc.DocId : 0,
                       BookingNo = item.BookingNo,
                       SendBackCode = item.SendBackCode,
                       SendBackName = item.SendBackName,
                       IsReceive = doc != null ? doc.IsReceive : default(bool?),
                       ReceiveDate = doc != null ? doc.ReceiveDate : default(DateTime?),
                       ReceiveBy = doc != null ? doc.ReceiveBy : default(int?),
                       ReceiveName = rec != null ? rec.FullName : null,
                       IsSend = doc != null ? doc.IsSend : default(bool?),
                       SendBy = doc != null ? doc.SendBy : default(int?),
                       SendName = sed != null ? sed.FullName : null,
                       SendDate = doc != null ? doc.SendDate : default(DateTime?),
                       Remark = doc != null ? doc.Remark : null
                     }).ToList();

      var res = new CarRegisItemRes
      {
        CarRegisListItemRes = list.ToList(),
        CarRegisListItemDocRes = listItemDoc.ToList()
      };

      return Ok(res);
    }
  }
}