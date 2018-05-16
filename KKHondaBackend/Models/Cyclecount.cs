using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class Cyclecount
    {
        public int Id { get; set; }
        public int? BranchId { get; set; }
        public int? Type { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
