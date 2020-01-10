using System;
namespace KKHondaBackend.Models
{
  public class CreditTransactionD
  {
    public int CTD_Id { get; set; }
    public int CTH_Id { get; set; }
    public int ContractItemId { get; set; }
    public string Description { get; set; }
    public decimal PayPrice { get; set; }
    public decimal PayVatPrice { get; set; }
    public decimal PayNetPrice { get; set; }
    public decimal FineSum { get; set; }
    public decimal FineSumOther { get; set; }
    public decimal RevenueStamp { get; set; }
    public decimal ComPrice { get; set; }
    public int DelayDueDate { get; set; }
  }
}
