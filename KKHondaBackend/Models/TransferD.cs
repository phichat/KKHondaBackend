using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class TransferD
    {
        public int Id { get; set; }
        public string TransferNo { get; set; }
        public string LineNo { get; set; }
        public string LineStatus { get; set; }
        public string BranchCodeOut { get; set; }
        public string BranchCodeIn { get; set; }
        public string LocationCodeOut { get; set; }
        public string LocationCodeIn { get; set; }
        public string StockIdFrom { get; set; }
        public string StockIdTo { get; set; }
        public decimal? TransferQty { get; set; }
        public decimal? TransferOutQty { get; set; }
        public decimal? TransferInQty { get; set; }
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
        public string LineRemark { get; set; }
    }
}
