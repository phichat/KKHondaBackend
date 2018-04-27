using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MProvince
    {
        public MProvince()
        {
            MAmphor = new HashSet<MAmphor>();
        }

        public string ProvinceCode { get; set; }
        public string ProvinceZone { get; set; }
        public string ProvinceNameTh { get; set; }
        public string ProvinceNameEn { get; set; }
        public string AbbrName { get; set; }
        public string IsoCode { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public ICollection<MAmphor> MAmphor { get; set; }
    }
}
