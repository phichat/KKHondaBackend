using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
  public partial class SaleReceipt
  {
    public int ReceiptId { get; set; }
    public string ReceiptNo { get; set; }
    public DateTime ReceiptDate { get; set; }
    public bool Status { get; set; }
    public string CustomerFullName { get; set; }
    public string CustomerAddress { get; set; }
    public string BranchTax { get; set; }
    public string Branch { get; set; }
    public string Reason { get; set; }
  }

}