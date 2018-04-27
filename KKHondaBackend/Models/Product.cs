using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class Product
    {
        public int ItemId { get; set; }
        public int? ItemType { get; set; }
        public int? CatId { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public int? ClassId { get; set; }
        public int? ColorId { get; set; }
        public int? ModelId { get; set; }
        public int? UnitId { get; set; }
        public string PartCode { get; set; }
        public string PartSpareCode { get; set; }
        public string PartName { get; set; }
        public string PartClass { get; set; }
        public string PartSource { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? CostVat { get; set; }
        public decimal? CostVatPrice { get; set; }
        public decimal? CostNet { get; set; }
        public decimal? SellPrice { get; set; }
        public decimal? SellVat { get; set; }
        public decimal? SellVatPrice { get; set; }
        public decimal? SellNet { get; set; }
        public int? ItemStatus { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
