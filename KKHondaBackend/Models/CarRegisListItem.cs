using System;
namespace KKHondaBackend.Models
{
  public partial class CarRegisListItem
  {
    public int RunId { get; set; }
    public int? BookingId { get; set; }
    public string ItemCode { get; set; }
    public string ItemName { get; set; }
    public string ItemTag { get; set; }
    public decimal? ItemPrice1 { get; set; }
    public decimal? ItemVatPrice1 { get; set; }
    public decimal? ItemNetPrice1 { get; set; }
    public decimal? ItemCutBalance { get; set; }
    public decimal? ItemPrice2 { get; set; }
    public decimal? ItemPrice3 { get; set; }
    public decimal? ItemPriceTotal { get; set; }
    public int? State { get; set; }
    public int? PaymentStatus { get; set; }
    public decimal? PaymentPrice { get; set; }
    public DateTime? DateReceipt { get; set; }
    public string Remark { get; set; }
  }
  public partial class CarRegisListItemRes
  {
    public int RunId { get; set; }
    public int? BookingId { get; set; }
    public string BookingNo { get; set; }
    public string ItemCode { get; set; }
    public string ItemName { get; set; }
    public string ItemTag { get; set; }
    public decimal? ItemPrice1 { get; set; }
    public decimal? ItemVatPrice1 { get; set; }
    public decimal? ItemNetPrice1 { get; set; }
    public decimal? ItemCutBalance { get; set; }
    public decimal? ItemPrice2 { get; set; }
    public decimal? ItemPrice3 { get; set; }
    public decimal? ItemPriceTotal { get; set; }
    public int? State { get; set; }
    public int? PaymentStatus { get; set; }
    public decimal? PaymentPrice { get; set; }
    public DateTime? DateReceipt { get; set; }
    public string Remark { get; set; }
  }
  public partial class CarRegisListItemSummary
  {
    public int BookingId { get; set; }
    public string BookingNo { get; set; }
    public DateTime BookingDate { get; set; }
    public decimal? ItemPrice1 { get; set; }
    public decimal? ItemVatPrice1 { get; set; }
    public decimal? ItemNetPrice1 { get; set; }
    public decimal? ItemCutBalance { get; set; }
    public decimal? ItemPrice2 { get; set; }
    public decimal? ItemPrice3 { get; set; }
    public decimal? ItemPriceTotal { get; set; }
    public decimal? PaymentPrice { get; set; }
  }
}