using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class CyclecountLocationItem
    {
        public int Runid { get; set; }
        public int? CId { get; set; }
        public int? WhlId { get; set; }
        public int? ItemId { get; set; }
        public int? CQty { get; set; }
    }
}
