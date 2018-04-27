using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class StockOpening
    {
        public int Id { get; set; }
        public string BranchCode { get; set; }
        public string GroupCode { get; set; }
        public string BrandCode { get; set; }
        public string CategoryCode { get; set; }
        public string ModelCode { get; set; }
        public string TypeCode { get; set; }
        public DateTime? OpeningDate { get; set; }
        public decimal? OpeningQty { get; set; }
        public decimal? ReceiptQty { get; set; }
        public decimal? TranferOutQty { get; set; }
        public decimal? TranferInQty { get; set; }
        public decimal? FreezeQty { get; set; }
        public decimal? AllocateQty { get; set; }
        public decimal? SaleQty { get; set; }
        public decimal? EndingQty { get; set; }
        public decimal? OtherPlusQty { get; set; }
        public decimal? OtherMinusQty { get; set; }
        public DateTime? StampDate { get; set; }
    }
}
