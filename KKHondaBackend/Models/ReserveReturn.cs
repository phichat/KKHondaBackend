using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
  public partial class ReserveReturn
  {
    public int RdId { get; set; }
    public int BookingId { get; set; }
    public string ReturnDepositNo { get; set; }
    public DateTime ReturnDepositDate { get; set; }
    public bool Status { get; set; }
    public string CustomerFullName { get; set; }
    public string CustomerAddress { get; set; }
    public int PaymentType { get; set; }
    public int? AccBankId { get; set; }
    public decimal? DiscountPrice { get; set; }
    public decimal TotalPaymentPrice { get; set; }
    public DateTime PaymentDate { get; set; }
    public string DocumentRef { get; set; }
    public string Remark { get; set; }
    public string Reason { get; set; }
  }
}
