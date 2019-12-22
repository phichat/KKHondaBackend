using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
  public partial class SaleInvTaxRec
  {
    public int InvTaxRecId { get; set; }
    public string InvTaxRecNo { get; set; }
    public DateTime InvTaxRecDate { get; set; }
    public bool Status { get; set; }
    public string CustomerFullName { get; set; }
    public string CustomerAddress { get; set; }
    public string BranchTax { get; set; }
    public string Branch { get; set; }
    public string Reason { get; set; }
  }
}
