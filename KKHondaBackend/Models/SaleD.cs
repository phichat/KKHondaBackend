using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class SaleD
    {
        public int Id { get; set; }
        public string ReserveNo { get; set; }
        public string SaleNo { get; set; }
        public string LineNo { get; set; }
        public string LineStatus { get; set; }
        public string LocationCode { get; set; }
        public string BranchCode { get; set; }
        public string GroupCode { get; set; }
        public string BrandCode { get; set; }
        public string CategoryCode { get; set; }
        public string ColorCode { get; set; }
        public string ModelCode { get; set; }
        public string TypeCode { get; set; }
        public string ModelName { get; set; }
        public string ModelDesc { get; set; }
        public string ModelUnit { get; set; }
        public string EngineNo { get; set; }
        public string FrameNo { get; set; }
        public string LotNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal? OrderQty { get; set; }
        public decimal? IssueQty { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? UnitCostTaxRate { get; set; }
        public decimal? UnitCostTaxAmt { get; set; }
        public decimal? UnitCostNet { get; set; }
        public string MarkupType { get; set; }
        public decimal? MarkupRate { get; set; }
        public decimal? MarkupAmt { get; set; }
        public string DiscountType { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? DiscountAmt { get; set; }
        public decimal? UnitSale { get; set; }
        public decimal? UnitSaleTaxRate { get; set; }
        public decimal? UnitSaleTaxAmt { get; set; }
        public decimal? UnitSaleNet { get; set; }
        public string LineRemark { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string FreePrb { get; set; }
        public string FreeRegister { get; set; }
        public string FreeInsurance { get; set; }
    }
}
