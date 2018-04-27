using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MGroupModel
    {
        public MGroupModel()
        {
            MItem = new HashSet<MItem>();
        }

        public string GroupCode { get; set; }
        public string ModelCode { get; set; }
        public string ModelCodeOld { get; set; }
        public string ModelName { get; set; }
        public string ModelDesc { get; set; }
        public string ModelUnit { get; set; }
        public string FactoryCode { get; set; }
        public string FactoryType { get; set; }
        public string Active { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public MGroup GroupCodeNavigation { get; set; }
        public ICollection<MItem> MItem { get; set; }
    }
}
