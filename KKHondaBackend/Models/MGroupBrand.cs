using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MGroupBrand
    {
        public MGroupBrand()
        {
            MItem = new HashSet<MItem>();
        }

        public string GroupCode { get; set; }
        public string BrandCode { get; set; }
        public string BrandCodeOld { get; set; }
        public string BrandNameTh { get; set; }
        public string BrandNameEn { get; set; }
        public string Active { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public MGroup GroupCodeNavigation { get; set; }
        public ICollection<MItem> MItem { get; set; }
    }
}
