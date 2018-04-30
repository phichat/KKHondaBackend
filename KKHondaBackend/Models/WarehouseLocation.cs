using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class WarehouseLocation
    {
        public int WhlId { get; set; }
        public int? WhId { get; set; }
        public int? BranchId { get; set; }
        public string WhlCode { get; set; }
        public string WhlName { get; set; }
        public int? WhlType { get; set; }
        public int? WhlStatus { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
