using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MGroupCategory
    {
        public MGroupCategory()
        {
            MItem = new HashSet<MItem>();
        }

        public string GroupCode { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryNameTh { get; set; }
        public string CategoryNameEn { get; set; }
        public string Active { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public MGroup GroupCodeNavigation { get; set; }
        public ICollection<MItem> MItem { get; set; }
    }
}
