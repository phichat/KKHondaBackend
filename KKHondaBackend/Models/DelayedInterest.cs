using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public class DelayedInterest
    {
        public Int64 Id { get; set; }
        public int InstalmentNo { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? Balance { get; set; }
        public decimal? FineSum { get; set; }
        public decimal? PayFineSum { get; set; }
        public decimal? Outstanding { get; set; }
        public int? DelayDueDate { get; set; }
        public string Remark { get; set; }
        public int?  ContractId { get; set; }
    }
}