using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
  public partial class StockReceive
  {
    public int ReceiveId { get; set; }
    public int? WhlId { get; set; }
    public int? BranchId { get; set; }
    public int? ItemId { get; set; }
    public int? ReceiveQty { get; set; }
    public int? ReceiveBy { get; set; }
    public DateTime? ReceiveDate { get; set; }
    public int? LogId { get; set; }
    public int? BalanceQty { get; set; }
    public int StockAllocate { get; set; }
    public int StockAviable { get; set; }
    public int StockOnhand { get; set; }
  }
}
