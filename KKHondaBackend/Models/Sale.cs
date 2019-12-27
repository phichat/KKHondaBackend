using System;

namespace KKHondaBackend.Models
{
  public partial class Sale
  {
    public int SaleId { get; set; }
    public int BookingId { get; set; }
    public decimal OutStandingPrice { get; set; }
    public decimal NetPrice { get; set; }
    public int SellTypeId { get; set; }
    public int SellAcitvityId { get; set; }
    public decimal Deposit { get; set; }
    public decimal DepositPrice { get; set; }
    public int InstalmentEnd { get; set; }
    public decimal InstalmentPrice { get; set; }
    public decimal InstalmentRemain { get; set; }
    public decimal Interest { get; set; }
    public decimal InterestPrice { get; set; }
    public decimal Remain { get; set; }
    public int TypePayment { get; set; }
    public DateTime FirstPayment { get; set; }
    public int DueDate { get; set; }
    public decimal? NowVat { get; set; }
    public decimal? PromotionalPrice { get; set; }
    public decimal? Irr { get; set; }
    public decimal? Mrr { get; set; }
    public int SaleBy { get; set; }
    public DateTime SaleDate { get; set; }
    public int? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public int? LogReceiveId { get; set; }
    public string Remark { get; set; }
    public string Reason { get; set; }
    public string SellNo { get; set; }
    public string ReturnDepositNo { get; set; }
    public string InvTaxRecNo { get; set; }
    public string ReceiptNo { get; set; }
    public string ComNo { get; set; }
    public bool? SellStatus { get; set; }
    public bool? ReturnDepositStatus { get; set; }
    public bool? InvTaxRecStatus { get; set; }
    public bool? ReceiptStatus { get; set; }
    public bool? ComStatus { get; set; }
    public int? ApproveId { get; set; }
  }

  public class SearchSale
  {
    public int? Status { get; set; }
    public string SellNo { get; set; }
    public DateTime? SellDate { get; set; }
    public string HireName { get; set; }
    public string HireIdCard { get; set; }
    public string ENo { get; set; }
    public string FNo { get; set; }
    public int? BookingPaymentType { get; set; }
    public int BranchId { get; set; }
  }

  public partial class SaleFormBody : Sale
  {
    public int ReturnDeposit { get; set; }
    public decimal ReturnDepositPrice { get; set; }
    public decimal? ComPrice { get; set; }
    public int? FiId { get; set; }
    public int? FiintId { get; set; }
    public int? FiComId { get; set; }

    public int PaymentType { get; set; }
    public decimal PaymentPrice { get; set; }
    public decimal? Discount { get; set; }
    public decimal TotalPaymentPrice { get; set; }
    public DateTime PaymentDate { get; set; }
    public int? AccBankId { get; set; }
    public string DocumentRef { get; set; }
  }

  public partial class CancelSlip
  {
    public string SlipNo { get; set; }
    public string Reason { get; set; }
    public int ApproveId { get; set; }
  }
}
