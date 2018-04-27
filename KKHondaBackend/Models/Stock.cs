using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class Stock
    {
        public string StockId { get; set; }
        public DateTime? StockDate { get; set; }
        public string StockStatus { get; set; }
        public string RefNo { get; set; }
        public string ReceiptNo { get; set; }
        public string BranchCode { get; set; }
        public string LocationCode { get; set; }
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
        public decimal? Qty { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? UnitCostTaxRate { get; set; }
        public decimal? UnitCostTaxAmt { get; set; }
        public decimal? UnitCostNet { get; set; }
        public string LineRemark { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
