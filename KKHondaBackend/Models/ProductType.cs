using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class ProductType
    {
        public int TypeId { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public int? TypeStatus { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
