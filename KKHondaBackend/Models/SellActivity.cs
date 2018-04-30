using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class SellActivity
    {
        public int ActivityId { get; set; }
        public string ActivityCode { get; set; }
        public string ActivityName { get; set; }
        public int SellTypeId { get; set; }
        public bool? ActiveStatus { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
