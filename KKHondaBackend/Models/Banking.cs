using System;
namespace KKHondaBackend.Models
{
  public class Banking
  {
    public int BankId { get; set; }
    public string BankCode { get; set; }
    public string BankName { get; set; }
    public bool Status { get; set; }
    public int CreateBy { get; set; }
    public DateTime CreateDate { get; set; }
    public int? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
  }

  public class BankingDetail
  {
    public string BankCode { get; set; }
    public string BankName { get; set; }
    public string AccBankNumber { get; set; }
    public string AccBankName { get; set; }
    public string AccBankType { get; set; }
    public string AccountType { get; set; }
  }
}
