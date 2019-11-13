using System;
using System.Collections.Generic;
using KKHondaBackend.Models;

namespace KKHondaBackend.Entities
{
  public class PaymentType
  {
    public const int Cash = 1;
    public const int Transfer = 2;
    public const int Cheque = 3;
    public const int CreditCard = 4;
    public static List<Status> Status = new List<Status> {
      new Status { Id = Cash, Desc = "เงินสด" },
      new Status { Id = Transfer, Desc = "เงินโอน" },
      new Status { Id = Cheque, Desc = "เช็ค" },
      new Status { Id = CreditCard, Desc = "บัตรเครดิต" }
    };
  }
  public class PaymentStatus
  {
    public const int Cancel = 0;
    public const int IsPayment = 1;
    public static List<Status> Status = new List<Status> {
      new Status { Id = Cancel, Desc = "ยกเลิก" },
      new Status { Id = IsPayment, Desc = "ชำระแล้ว" },
    };
  }
  public class ConStatus1
  {
    public const int Cancel = 0;
    public const int Received = 1;
    public const int Withdraw1 = 2;
    public const int Withdraw2 = 3;
    // public const int Sending = 4;
    // public const int PartialDelivery = 5;
    // public const int CompleteDelivery = 6;
    public static List<Status> Status = new List<Status>
        {
            new Status { Id = 0, Desc = "ยกเลิก" },
            new Status { Id = 1, Desc = "รับเอกสารเข้าระบบ" },
            new Status { Id = 2, Desc = "เบิกเงินครั้งที่ 1" },
            new Status { Id = 3, Desc = "เบิกเงินครั้งที่ 2" },
            // new Status { Id = 4, Desc = "ส่งเรื่องดำเนินการ" },
            // new Status { Id = 5, Desc = "ส่งมอบ (บางส่วน)" },
            // new Status { Id = 6, Desc = "ส่งมอบ (ครบ)" },
        };
  }

  public class ConStatus2
  {
    public const int Send1 = 1;
    public const int Send2 = 2;
    public const int REV = 3;
    public static List<Status> Status = new List<Status>
        {
            new Status { Id = 1, Desc = "ส่งเรื่องครั้งที่ 1" },
            new Status { Id = 2, Desc = "ส่งเรื่องครั้งที่ 2" },
            new Status { Id = 3, Desc = "บันทึกรับคืนเรื่อง" }
        };
  }

  public class SedStatus
  {
    public const int Normal = 1;
    public const int Borrowed = 2;
    public const int Received = 3;
    public const int Cancel = 0;
    public static List<Status> Status = new List<Status>
        {
            new Status { Id = 1, Desc = "ปกติ" },
            new Status { Id = 2, Desc = "บันทึกการยืมเงิน"},
            new Status { Id = 3, Desc = "บันทึกรับคืนเรื่อง"},
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

  public class ClStatus
  {
    public const int Cancel = 0;
    public const int Normal = 1;
    public static List<Status> Status = new List<Status>
        {
            new Status { Id = Cancel, Desc = "ยกเลิก"},
            new Status { Id = Normal, Desc = "ปกติ"}
        };
  }

  public class ReceiveClStatus
  {
    public const int Cancel = 0;
    public const int Normal = 1;
    public static List<Status> Status = new List<Status>
        {
            new Status { Id = Cancel, Desc = "ยกเลิก"},
            new Status { Id = Normal, Desc = "ปกติ"}
        };
  }

  public class RevStatus
  {
    public const int Cancel = 0;
    public const int Normal = 1;
    public static List<Status> Status = new List<Status>
        {
            new Status { Id = 0, Desc = "ยกเลิก"},
            new Status { Id = 1, Desc = "ปกติ"}
        };
  }

  public class ExpensesType
  {
    public const int Service = 1;
    public const int Expenses = 2;
    public const int InternalCost = 3;

    public static List<Status> Status = new List<Status>
        {
            new Status { Id = 1, Desc = "บริการ"},
            new Status { Id = 2, Desc = "ค่าบริการ"},
            new Status { Id = 3, Desc = "ค่าใช้จ่ายภายใน"}
        };
  }

  public class ExpensesStatus
  {
    public const bool Active = true;
    public const bool Deactivate = false;
    public static List<Status> Status = new List<Status>
        {
            new Status { Id = 0, Desc = "ยกเลิก"},
            new Status { Id = 1, Desc = "ใช้งาน"}
        };
  }

  public static class ExpensesTag
  {
    public const string EXP10001 = "EXP10001";
    public const string EXP10002 = "EXP10002";
    public const string EXP10003 = "EXP10003";
    public const string EXP10004 = "EXP10004";
    public class ExpenseStatus
    {
      public string Id { get; set; }
      public string Desc { get; set; }
    };
    public static List<ExpenseStatus> Status = new List<ExpenseStatus>
    {
        new ExpenseStatus { Id = EXP10001, Desc = "จดทะเบียนรถใหม่"},
        new ExpenseStatus { Id = EXP10002, Desc = "ต่อทะเบียน"},
        new ExpenseStatus { Id = EXP10003, Desc = "พ.ร.บ."},
        new ExpenseStatus { Id = EXP10004, Desc = "ประกันภัย"}
    };
  }

}