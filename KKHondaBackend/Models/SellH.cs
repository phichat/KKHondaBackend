using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class SellH
    {
        public int Id { get; set; }
        public string RefNo { get; set; }
        public string BranchCode { get; set; }
        public string ReserveNo { get; set; }
        public string SaleNo { get; set; }
        public string SaleStatus { get; set; }
        public string SaleType { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string AllocatedBy { get; set; }
        public DateTime? AllocatedDate { get; set; }
        public string DeliveredBy { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerPrename { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerPhone { get; set; }
        public string RegisterCode { get; set; }
        public string RegisterPrename { get; set; }
        public string RegisterName { get; set; }
        public string RegisterSurname { get; set; }
        public string RegisterAddress { get; set; }
        public string ShipAddress { get; set; }
        public string ShipAmphorCode { get; set; }
        public string ShipProvinceCode { get; set; }
        public string ShipZipcode { get; set; }
        public DateTime? ReserveDate { get; set; }
        public string ReserveStatus { get; set; }
        public decimal? ReserveAmt { get; set; }
        public decimal? TotalPrice { get; set; }
        public string MarkupType { get; set; }
        public decimal? MarkupRate { get; set; }
        public decimal? MarkupAmt { get; set; }
        public string DiscountType { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? DiscountAmt { get; set; }
        public decimal? TotalPriceExcludeTax { get; set; }
        public decimal? TaxRate { get; set; }
        public decimal? TaxAmt { get; set; }
        public decimal? TotalNetPrice { get; set; }
        public string Remark { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string RegisterPhone { get; set; }
    }
}
