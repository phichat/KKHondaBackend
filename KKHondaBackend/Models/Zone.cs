using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class Zone
    {
        public int ZoneId { get; set; }
        public string ZoneCode { get; set; }
        public string ZoneName { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
