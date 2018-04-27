using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MWorkgroup
    {
        public string WorkgroupCode { get; set; }
        public string WorkgroupName { get; set; }
        public string WorkgroupDesc { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
