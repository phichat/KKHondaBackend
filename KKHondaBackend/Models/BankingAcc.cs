using System;

namespace KKHondaBackend.Models
{
  public partial class BankingAcc
  {
    public int AccBankId { get; set; }
    public string AccBankCode { get; set; }
    public string AccBankNumber { get; set; }
    public string AccBankName { get; set; }
    public string AccBankType { get; set; }
    public string AccountType { get; set; }
    public int CreateBy { get; set; }
    public DateTime CreateDate { get; set; }
    public int? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
  }
}