using System;
namespace KKHondaBackend.Models
{
  public class CreditTransaction
  {
    public int TransactionId { get; set; }
    public string ReceiptNo { get; set; }
    public string TaxInvNo { get; set; }
    public int ContractItemId { get; set; }
    public string Description { get; set; }
    public decimal PayPrice { get; set; }
    public decimal PayVatPrice { get; set; }
    public decimal PayNetPrice { get; set; }
 
    public decimal FineSum { get; set; }
    public decimal FineSumOther { get; set; }
    public decimal RevenueStamp { get; set; }
    public string PaymentName { get; set; }
    public int DelayDueDate { get; set; }
    public int? AccBankId { get; set; }
    public int Payeer { get; set; }
    public int PaymentType { get; set; }
    public string DocumentRef { get; set; }
    public decimal PaymentPrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public decimal TotalPaymentPrice { get; set; }
    public DateTime? PaymentDate { get; set; }
    public int BranchId { get; set; }
    public string Remark { get; set; }
    public string Reason { get; set; }
    public DateTime CreateDate { get; set; }
    public int CreateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public int UpdateBy { get; set; }
  }
}
