using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
  public partial class CarRegisClDeposit
  {
    public int Id { get; set; }
    public string ListBookingId { get; set; }
    public string ExpenseTag { get; set; }
    public string InsuranceCode { get; set; }
    public string ReceiptNo { get; set; }
    public DateTime? ReceiptDate { get; set; }
    public decimal TotalNetPrice1 { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal TotalPrice { get; set; }
    public int PaymentType { get; set; }
    public decimal PaymentPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPaymentPrice { get; set; }
    public DateTime? PaymentDate { get; set; }
    public int? AccBankId { get; set; }
    public string DocumentRef { get; set; }
    public int Status { get; set; }
    public int BranchId { get; set; }
    public int CreateBy { get; set; }
    public DateTime CreateDate { get; set; }
    public int? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string Reason { get; set; }
    public string Remark { get; set; }
  }

  public partial class CarRegisClDepositRes : CarRegisClDeposit
  {
    public string ExpenseTagName { get; set; }
    public string InsuranceName { get; set; }
    public string PaymentTypeDesc { get; set; }
    public string BankName { get; set; }
    public string StatusDesc { get; set; }
    public string BranchName { get; set; }
    public string CreateName { get; set; }
    public string UpdateName { get; set; }
  }
  public partial class CarRegisClDepositFormBody
  {
    public string ExpenseTag { get; set; }
    public string InsuranceCode { get; set; }
    public int PaymentType { get; set; }
    public decimal PaymentPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPaymentPrice { get; set; }
    public DateTime? PaymentDate { get; set; }
    public int? AccBankId { get; set; }
    public string DocumentRef { get; set; }
    public int Status { get; set; }
    public int BranchId { get; set; }
    public int CreateBy { get; set; }
    public string Reason { get; set; }
    public string Remark { get; set; }
    public List<CarRegisClDepositDeposit> ConList { get; set; }
  }

  public partial class CarRegisClDepositDetail
  {
    public string ExpenseTag { get; set; }
    public string InsuranceCode { get; set; }
    public string InsuranceName { get; set; }
    public int PaymentType { get; set; }
    public decimal PaymentPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPaymentPrice { get; set; }
    public DateTime? PaymentDate { get; set; }
    public int? AccBankId { get; set; }
    public string BankName { get; set; }
    public string AccBankNumber { get; set; }
    public string AccBankName { get; set; }
    public string DocumentRef { get; set; }
    public int Status { get; set; }
    public string StatusDesc { get; set; }
    public int BranchId { get; set; }
    public int CreateBy { get; set; }
    public string CreateName { get; set; }
    public string Reason { get; set; }
    public string Remark { get; set; }
    public string ReceiptNo { get; set; }
    public DateTime? ReceiptDate { get; set; }
    public List<CarRegisClDepositDeposit> ConList { get; set; }
  }
  public partial class CarRegisClDepositDeposit
  {
    public int BookingId { get; set; }
    public string BookingNo { get; set; }
    public DateTime BookingDate { get; set; }
    public decimal NetPrice1 { get; set; }
    public decimal Expense { get; set; }
    public decimal PaymentPrice { get; set; }
  }

  public partial class CarRegisClDepositCancel
  {
    public int Id { get; set; }
    public int UpdateBy { get; set; }
    public string Reason { get; set; }
  }

  public partial class SearchClDepositList
  {
    public string ReceiptNo { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string[] ExpenseTag { get; set; }
    public int?[] PaymentType { get; set; }
    public string CreateName { get; set; }
    public int? Status { get; set; }
  }
}