using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class GroupPage
    {
        public int GId { get; set; }
        public string GName { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? BranchId { get; set; }
    }
}
