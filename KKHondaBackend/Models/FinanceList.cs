using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class FinanceList
    {
        public int FiId { get; set; }
        public string FiCode { get; set; }
        public int? FiFix { get; set; }
        public int? BranchId { get; set; }
        public int? FiStatus { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
