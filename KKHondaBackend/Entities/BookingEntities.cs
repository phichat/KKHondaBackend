using System;
using System.Collections.Generic;
using KKHondaBackend.Models;

namespace KKHondaBackend.Entities
{
    public class BookingPaymentType
    {
        public const int Cash = 1;
        public const int Leasing = 2;
        public const int HierPurchase = 3;
        public const int Credit = 4;
        public static List<Status> Status = new List<Status> {
            new Status { Id = 1, Desc = "เงินสด" },
            new Status { Id = 2, Desc = "ไฟแนนซ์" },
            new Status { Id = 3, Desc = "เช่าซื้อ" },
            new Status { Id = 4, Desc = "ขายเชื่อ" }
        };
    }

    public class BookingStatus
    {
        public const int Booking = 1;
        public const int Sell = 2;
        public const int Cancel = 9;
        public static List<Status> Status = new List<Status> {
            new Status { Id = 1, Desc = "จอง" },
            new Status { Id = 2, Desc = "ขาย" },
            new Status { Id = 9, Desc = "ยกเลิก" },
        };
    }
}