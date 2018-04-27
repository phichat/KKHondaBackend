using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class ProductModel
    {
        public int ModelId { get; set; }
        public string ModelCode { get; set; }
        public string ModelType { get; set; }
        public string ModelRefCode { get; set; }
        public string ModelName { get; set; }
        public int? ModelStatus { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
