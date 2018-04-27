using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class Warehouse
    {
        public int WhId { get; set; }
        public string WhCode { get; set; }
        public int? BranchId { get; set; }
        public string WhName { get; set; }
        public int? WhStatus { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
