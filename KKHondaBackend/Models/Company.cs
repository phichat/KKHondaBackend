using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class Company
    {
        public int ComId { get; set; }
        public string ComCode { get; set; }
        public string ComName { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
