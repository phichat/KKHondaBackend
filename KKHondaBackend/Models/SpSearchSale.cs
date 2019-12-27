using System;
using System.ComponentModel.DataAnnotations;

namespace KKHondaBackend.Models
{
  public partial class SpSearchSale
  {
    [Key]
    public int ContractId { get; set; }
    public int BookingId { get; set; }
    public int SaleId { get; set; }
    public string Branch { get; set; }
    public string ContractNo { get; set; }
    public string SellNo { get; set; }
    public string StatusDesc { get; set; }
    public int ContractStatus { get; set; }
    public string RefNo { get; set; }
    public int BookingPaymentType { get; set; }
    public string ContractHire { get; set; }
    public string HireFullName { get; set; }
    public string ContractOwner { get; set; }
    public string OwnerFullName { get; set; }
    public string HireIdCard { get; set; }
    public string Brand { get; set; }
    public string Color { get; set; }
    public string Model { get; set; }
    public string EngineNo { get; set; }
    public string FrameNo { get; set; }
    public DateTime? EndContractDate { get; set; }
    public decimal NetPrice { get; set; }
    public DateTime SaleDate { get; set; }
    public string SaleFullName { get; set; }
    public string SaleRemark { get; set; }
  }
}