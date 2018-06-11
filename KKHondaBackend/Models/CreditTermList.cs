using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class CreditTermList
    {
        public int CtId { get; set; }
        public int? CtNo { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
