
using System;
using System.ComponentModel.DataAnnotations;

namespace KKHondaBackend.Models
{
  public class SpSearchContractHps
  {
    [Key]
    public int ContractId { get; set; }
    public int CalculateId { get; set; }
    public string Branch { get; set; }
    public int? BookingPaymentType { get; set; }
    public string ContractNo { get; set; }
    public string ContractType { get; set; }
    public DateTime? ContractDate { get; set; }
    public string AreaPayment { get; set; }
    public string ContractPoint { get; set; }
    public string ContractGroup { get; set; }
    public string StatusDesc { get; set; }
    public int? ContractStatus { get; set; }
    public string RefNo { get; set; }
    public string HireFullName { get; set; }
    public string HireIdCard { get; set; }
    public string Brand { get; set; }
    public string Color { get; set; }
    public string Model { get; set; }
    public string EngineNo { get; set; }
    public string FrameNo { get; set; }
    public DateTime? EndContractDate { get; set; }
  }
}