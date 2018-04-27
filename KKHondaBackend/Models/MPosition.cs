using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MPosition
    {
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        public string PositionDesc { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
