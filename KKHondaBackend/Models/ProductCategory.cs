using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class ProductCategory
    {
        public int CatId { get; set; }
        public string CatCode { get; set; }
        public string CatName { get; set; }
        public int? CatStatus { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
