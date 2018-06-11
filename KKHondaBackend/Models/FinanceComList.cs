using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class FinanceComList
    {
        public int FicomId { get; set; }
        public int? FiId { get; set; }
        public int? FiintId { get; set; }
        public int? MinCtId { get; set; }
        public int? MaxCtId { get; set; }
        public decimal? MinDown { get; set; }
        public decimal? MaxDown { get; set; }
        public decimal? ComPrice { get; set; }
    }
}
