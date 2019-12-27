using System;
using System.Collections.Generic;
using KKHondaBackend.Models;

namespace KKHondaBackend.Entities
{


    public class DealerPurchase
    {
        public static List<PurchaseDealer> dealer = new List<PurchaseDealer> {
          new PurchaseDealer { Id = 2, Desc = "เอ พี ฮอนด้า จำกัด" }
        };
    }

    public class StatusPurchase
    {
        public static List<PurchaseStatus> status = new List<PurchaseStatus> {
          new PurchaseStatus { Id = 1, Desc = "ยังไม่รับของ" },
          new PurchaseStatus { Id = 2, Desc = "รับของแล้ว" },
        };
    }

    public class DealerName
    {
        public static List<ListDealer> listDealer = new List<ListDealer> {
          new ListDealer { Id = 2, Desc = "เอ พี ฮอนด้า จำกัด" }
        };
    }

    public class ReceiveStatus
    {
        public static List<ListStatus> listStatus = new List<ListStatus> {
          new ListStatus { Id = 1, Desc = "ใหม่" },
          new ListStatus { Id = 2, Desc = "จ่ายเงินครบแล้ว" },
        };
    }

    public class ReceiveLineStatus
    {
        public static List<ListLineStatus> listLineStatus = new List<ListLineStatus> {
          new ListLineStatus { Id = 1, Desc = "ใหม่" },
          new ListLineStatus { Id = 2, Desc = "จ่ายเงินครบแล้ว" },
        };
    }

    public class ReceiveType
    {
        public static List<ListType> listType = new List<ListType> {
          new ListType { Id = 1, Desc = "สินค้าใหม่" },
          new ListType { Id = 2, Desc = "สินค้าเก่า" },
        };
    }

}