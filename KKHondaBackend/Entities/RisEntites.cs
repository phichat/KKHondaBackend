using System;
using System.Collections.Generic;
using KKHondaBackend.Models;

namespace KKHondaBackend.Entities
{
    public class PaymentType
    {
        public const int Cash = 1;
        public const int Transfer = 2;
        public static List<Status> Status = new List<Status> {
            new Status { Id = 1, Desc = "เงินสด" },
            new Status { Id = 2, Desc = "เงินโอน" }
        };
    }
    public class ConStatus
    {
        public const int Cancel = 0;
        public const int Received = 1;
        public const int Sending = 2;
        public const int PartialDelivery = 3;
        public const int CompleteDelivery = 4;
        public static List<Status> Status = new List<Status>
        {
            new Status { Id = 0, Desc = "ยกเลิก" },
            new Status { Id = 1, Desc = "รับเอกสารเข้าระบบ" },
            new Status { Id = 2, Desc = "สรุปเรื่องส่งดำเนินการ" },
            new Status { Id = 3, Desc = "ส่งมอบ (บางส่วน)" },
            new Status { Id = 4, Desc = "ส่งมอบ (ครบ)" },
        };
    }

    public class SedStatus
    {
        public const int Normal = 1;
        public const int Borrowed = 2;
        public const int Cancel = 0;
        public static List<Status> Status = new List<Status>
        {
            new Status { Id = 1, Desc = "ปกติ" },
            new Status { Id = 2, Desc = "บันทึกการยืมเงิน"},
            new Status { Id = 0, Desc = "ยกเลิก"}
        };
    }

    public class AlStatus
    {
        public const int Cancel = 0;
        public const int Normal = 1;
        public const int CashBack = 2;
        public static List<Status> Status = new List<Status>
        {
            // new Status { Id = 0, Desc = "รอดำเนินการ" },
            new Status { Id = 1, Desc = "ปกติ"},
            new Status { Id = 2, Desc = "บันทึกคืนเงิน"},
            new Status { Id = 0, Desc = "ยกเลิก"}
        };
    }

    public class ClStatus {
        public const int Cancel = 0;
        public const int Normal = 1;
        public static List<Status> Status = new List<Status>
        {
            new Status { Id = 0, Desc = "ยกเลิก"},
            new Status { Id = 1, Desc = "ปกติ"}
        };
    }

    public class ExpensesType {
        public const int Service = 1;
        public const int Expenses = 2;

        public static List<Status> Status = new List<Status>
        {
            new Status { Id = 1, Desc = "บริการ"},
            new Status { Id = 2, Desc = "ค่าบริการ"}
        };
    }

    public class ExpensesStatus {
        public const bool Active = true;
        public const bool Deactivate = false;
        public static List<Status> Status = new List<Status>
        {
            new Status { Id = 0, Desc = "ยกเลิก"},
            new Status { Id = 1, Desc = "ใช้งาน"}
        };
    }
}