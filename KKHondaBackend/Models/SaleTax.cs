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
    public string CustomerFullName { get; set; }
    public string CustomerAddress { get; set; }
    public string BranchTax { get; set; }
    public string Branch { get; set; }
    public string Reason { get; set; }
  }
}