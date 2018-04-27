using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class ProductClass
    {
        public int ClassId { get; set; }
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public int? ClassStatus { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
