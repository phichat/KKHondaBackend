using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class TransactionLog
    {
        public int RunningId { get; set; }
        public string TransId { get; set; }
        public DateTime? TransDate { get; set; }
        public string TransType { get; set; }
        public decimal? BfPhyQty { get; set; }
        public decimal? BfFltQty { get; set; }
        public decimal? TransQty { get; set; }
        public decimal? AfPhyQty { get; set; }
        public decimal? AfFltQty { get; set; }
        public string StockId { get; set; }
        public string RefNo { get; set; }
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
        public decimal? UnitCost { get; set; }
        public decimal? UnitCostTaxRate { get; set; }
        public decimal? UnitCostTaxAmt { get; set; }
        public decimal? UnitCostNet { get; set; }
        public decimal? UnitSale { get; set; }
        public decimal? UnitSaleTaxRate { get; set; }
        public decimal? UnitSaleTaxAmt { get; set; }
        public decimal? UnitSaleNet { get; set; }
        public string LineRemark { get; set; }
        public string TransBy { get; set; }
    }
}
