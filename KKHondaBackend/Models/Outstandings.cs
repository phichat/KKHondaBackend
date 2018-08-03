using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models {
    public class Outstandings {
        public int Id { get; set; }
        public decimal FineSum { get; set; }
        public decimal Deposit { get; set; }
        public decimal Balance { get; set; }
        public int StartInstalment { get; set; }
        public int EndInstalment { get; set; }
        public decimal OutstandingTotal { get; set; }
        public decimal PayPriceTotal { get; set; }
        public int NextInstalment { get; set; }
        public decimal NextInstalmentBalance { get; set; }
        public DateTime NextDueDate { get; set; }
        public int FutureInstalment { get; set; }
        public decimal FutureInstalmentBalance { get; set; }
    }
}