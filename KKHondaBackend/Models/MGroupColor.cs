using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MGroupColor
    {
        public MGroupColor()
        {
            MItem = new HashSet<MItem>();
        }

        public string GroupCode { get; set; }
        public string ColorCode { get; set; }
        public string ColorNameTh { get; set; }
        public string ColorNameEn { get; set; }
        public string Active { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public MGroup GroupCodeNavigation { get; set; }
        public ICollection<MItem> MItem { get; set; }
    }
}
