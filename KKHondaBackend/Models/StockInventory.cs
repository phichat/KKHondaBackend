using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class StockInventory
    {
        public int Id { get; set; }
        public string BranchCode { get; set; }
        public string GroupCode { get; set; }
        public string BrandCode { get; set; }
        public string CategoryCode { get; set; }
        public string TypeCode { get; set; }
        public string ModelCode { get; set; }
        public decimal? PhyQty { get; set; }
        public decimal? FltQty { get; set; }
    }
}
