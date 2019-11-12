using System;
namespace KKHondaBackend.Models
{
  public partial class CarRegisAlList
  {
    public int AlId { get; set; }
    public string AlNo { get; set; }
    public string SedNo { get; set; }
    public decimal Price2Remain { get; set; }
    public int? PaymentType { get; set; }
    public decimal PaymentPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
    public decimal TotalPaymentPrice { get; set; }
    public int? AccBankId { get; set; }
    public DateTime PaymentDate { get; set; }
    public string DocumentRef { get; set; }
    public string Remark { get; set; }
    public int? Status { get; set; }
    public int BranchId { get; set; }
    public DateTime CreateDate { get; set; }
    public int CreateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public int? UpdateBy { get; set; }
    public string Reason { get; set; }

  }

  public partial class CarRegisAlListRes
  {
    public int AlId { get; set; }
    public string AlNo { get; set; }
    public string SedNo { get; set; }
    public string BorrowerName { get; set; }
    public decimal BalancePrice { get; set; }
    public decimal ReceivePrice { get; set; }
    public decimal NetPrice { get; set; }
    public decimal Price2Remain { get; set; }
    public int? PaymentType { get; set; }
    public decimal PaymentPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
    public decimal TotalPaymentPrice { get; set; }
    public int? AccBankId { get; set; }
    public string BankName { get; set; }
    public string AccBankName {get;set;}
    public string AccBankNumber { get; set; }
    public DateTime PaymentDate { get; set; }
    public string DocumentRef { get; set; }
    public string Remark { get; set; }
    public int? Status { get; set; }
    public string StatusDesc { get; set; }
    public int BranchId { get; set; }
    public string BranchName { get; set; }
    public DateTime CreateDate { get; set; }
    public int CreateBy { get; set; }
    public string CreateName { get; set; }
    public DateTime? UpdateDate { get; set; }
    public int? UpdateBy { get; set; }
    public string UpdateName { get; set; }
    public string Reason { get; set; }
  }

  public partial class CarRegisAlCancel
  {
    public string AlNo { get; set; }
    public string Reason { get; set; }
    public string Remark { get; set; }
    public int UpdateBy { get; set; }
  }

  public partial class SearchAlList {
     public string SedNo { get; set; }
     public string AlNo { get; set; }
    public DateTime? CreateDate { get; set; }
    public string BorrowerName {get;set;}
    public string CreateName { get; set; }
    public int? Status { get; set; }
  }
}