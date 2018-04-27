using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MGroup
    {
        public MGroup()
        {
            MGroupBrand = new HashSet<MGroupBrand>();
            MGroupCategory = new HashSet<MGroupCategory>();
            MGroupColor = new HashSet<MGroupColor>();
            MGroupModel = new HashSet<MGroupModel>();
            MGroupType = new HashSet<MGroupType>();
            MItem = new HashSet<MItem>();
        }

        public string GroupCode { get; set; }
        public string GroupNameTh { get; set; }
        public string GroupNameEn { get; set; }
        public string Active { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public ICollection<MGroupBrand> MGroupBrand { get; set; }
        public ICollection<MGroupCategory> MGroupCategory { get; set; }
        public ICollection<MGroupColor> MGroupColor { get; set; }
        public ICollection<MGroupModel> MGroupModel { get; set; }
        public ICollection<MGroupType> MGroupType { get; set; }
        public ICollection<MItem> MItem { get; set; }
    }
}
