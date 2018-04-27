using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class SellD
    {
        public int Id { get; set; }
        public int? SellHId { get; set; }
        public bool? LineStatus { get; set; }
        public int? LocationId { get; set; }
        public int? BranchId { get; set; }
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public int? ModelId { get; set; }
        public int? ColorId { get; set; }
        public int? CateId { get; set; }
        public int? ClassId { get; set; }
        public string EngineNo { get; set; }
        public string FrameNo { get; set; }
        public string UnitId { get; set; }
        public int? OrderQty { get; set; }
        public int? IssueQty { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? CostVat { get; set; }
        public decimal? CostVatPrice { get; set; }
        public decimal? CostNet { get; set; }
        public decimal? SellPrice { get; set; }
        public decimal? SellVat { get; set; }
        public decimal? SellVatPrice { get; set; }
        public decimal? SellNet { get; set; }
        public decimal? DiscountPrice { get; set; }
        public decimal? DiscountVat { get; set; }
        public string Remark { get; set; }
        public bool? FreePrb { get; set; }
        public bool? FreeRegister { get; set; }
        public bool? FreeInsurance { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
