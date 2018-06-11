using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class CyclecountScan
    {
        public int ScanId { get; set; }
        public int? CId { get; set; }
        public int? WhlId { get; set; }
        public int? RefId { get; set; }
        public int? SQty { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
