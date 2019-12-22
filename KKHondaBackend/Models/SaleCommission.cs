using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
  public partial class SaleCommission
  {
    public int ComId { get; set; }
    public string ComNo { get; set; }
    public DateTime ComDate { get; set; }
    public decimal ComPrice { get; set; }
    public bool Status { get; set; }
    public string CustomerFullName { get; set; }
    public string CustomerAddress { get; set; }
    public string BranchTax { get; set; }
    public string Branch { get; set; }
    public string Reason { get; set; }
    public int FiId { get; set; }
    public int FiintId { get; set; }
    public int FiComId { get; set; }
  }

}