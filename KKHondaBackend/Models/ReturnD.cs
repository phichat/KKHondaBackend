using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class ReturnD
    {
        public int id { get; set; }
        public string return_no { get; set; }
        public string receive_no { get; set; }
        public string tax_invoice_no { get; set; }    
        public int? item_id { get; set; }
        public int? log_id { get; set; }
        public int? return_qty { get; set; }
        public decimal? return_amt { get; set; }
    }
    

}