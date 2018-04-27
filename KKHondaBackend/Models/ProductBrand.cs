using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class ProductBrand
    {
        public int BrandId { get; set; }
        public string BrandCode { get; set; }
        public string BrandRefCode { get; set; }
        public string BrandName { get; set; }
        public int? BrandStatus { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
