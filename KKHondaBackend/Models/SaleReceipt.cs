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
    public string CustomerCode { get; set; }
    public string CustomerFullName { get; set; }
    public string CustomerFullAddress { get; set; }
    public string BranchTax { get; set; }
    public string Branch { get; set; }
    public string Reason { get; set; }
    public int? ApproveId { get; set; }
    public int? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
  }

}