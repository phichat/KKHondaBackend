using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MAmphor
    {
        public string ProvinceCode { get; set; }
        public string AmphorCode { get; set; }
        public string AmphorName { get; set; }
        public string AmphorZone { get; set; }
        public string Zipcode { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public MProvince ProvinceCodeNavigation { get; set; }
    }
}
