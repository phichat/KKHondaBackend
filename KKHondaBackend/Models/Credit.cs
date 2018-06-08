using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public class Credit
    {
        public CreditCalculate creditCalculate { get; set; }
        public CreditContract creditContract { get; set; }
        public CreditContractItem[] creditContactItem { get; set; }

    }
}
