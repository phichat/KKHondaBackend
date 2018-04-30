using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class SellType
    {
        public int SellTypeId { get; set; }
        public string SellTypeCode { get; set; }
        public string SellTypeName { get; set; }
        public bool? SellTypeActive { get; set; }
        public int CreateBy { get; set; }
        public string CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
