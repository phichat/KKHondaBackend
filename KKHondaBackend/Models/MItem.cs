using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MItem
    {
        public string GroupCode { get; set; }
        public string CategoryCode { get; set; }
        public string TypeCode { get; set; }
        public string ModelCode { get; set; }
        public string BrandCode { get; set; }
        public string ColorCode { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? UnitCostTaxRate { get; set; }
        public decimal? UnitCostTaxAmt { get; set; }
        public decimal? UnitCostNet { get; set; }
        public string Active { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public MGroup GroupCodeNavigation { get; set; }
        public MGroupBrand MGroupBrand { get; set; }
        public MGroupCategory MGroupCategory { get; set; }
        public MGroupColor MGroupColor { get; set; }
        public MGroupModel MGroupModel { get; set; }
        public MGroupType MGroupType { get; set; }
    }
}
