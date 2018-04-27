using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class ProductColor
    {
        public int ColorId { get; set; }
        public string ColorCode { get; set; }
        public string ColorName { get; set; }
        public int? ColorStatus { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
