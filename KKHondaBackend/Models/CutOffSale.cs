using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models {
    public class CutOffSale {
        public int Id { get; set; }
        public decimal InterestInstalment { get; set; }
        public decimal GoodPrice { get; set; }
        public decimal Balance { get; set; }
        public decimal DepositPrice { get; set; }
        public decimal FineSum { get; set; }
        public decimal SumBalance { get; set; }
        public decimal Discount { get; set; }
        public decimal DistCutOffSale { get; set; }
        public decimal SumDiscount { get; set; }
        public decimal TotalBalance { get; set; }
    }
}