using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models {
    public class HistoryPayment {
        public Int64? Id { get; set; }
        public string ContractNo { get; set; }
        public DateTime? ContractDate { get; set; }
        public int? InstalmentEnd { get; set; }
        public int? PayBefore { get; set; }
        public int? PayMatch { get; set; }
        public int? PayLate { get; set; }
        public decimal? RateLate { get; set; }
        public string Grade { get; set; }
    }
}