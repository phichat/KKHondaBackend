using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
  public partial class SaleTax
  {
    public int TaxId { get; set; }
    public string TaxNo { get; set; }
    public DateTime TaxDate { get; set; }
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