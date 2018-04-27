using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class Sellunit
    {
        public int UnitId { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
