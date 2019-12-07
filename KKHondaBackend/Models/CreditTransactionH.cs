using System;

namespace KKHondaBackend.Models
{
  public partial class CreditTransactionH
  {
    public int CTH_Id { get; set; }
    public int ContractId { get; set; }
    public string ReceiptNo { get; set; }
    public string TaxInvNo { get; set; }
    public int? AccBankId { get; set; }
    public int Payeer { get; set; }
    public int PaymentType { get; set; }
    public string DocumentRef { get; set; }
    public int BranchId { get; set; }
    public string PaymentName { get; set; }
    public decimal OutstandingBalance { get; set; }
    public decimal DiscountInterest { get; set; }
    public decimal CutBalance { get; set; }
    public decimal PaymentPrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public decimal TotalPaymentPrice { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Reason { get; set; }
    public string Remark { get; set; }
    public int Status { get; set; }
    public int? ApproveBy { get; set; }
    public DateTime CreateDate { get; set; }
    public int CreateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public int? UpdateBy { get; set; }
  }

  public class CreditTransactionReceipt
  {
    public string ReceiptNo { get; set; }
    public string TaxInvNo { get; set; }

  }
}