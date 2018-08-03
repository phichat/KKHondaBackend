using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models {
     public class Discounts
        {
            public Int64 Id { get; set; }
            public int InstalmentNo { get; set; }
            public DateTime DueDate { get; set; }
            public decimal Balance { get; set; }
            public decimal Outstanding { get; set; }
            public decimal Discount { get; set; }
            public decimal UseDiscount { get; set; }
        }
}