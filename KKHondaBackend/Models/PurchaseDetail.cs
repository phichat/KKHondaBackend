using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{

    public partial class PurchaseDetail
    {
        public int id { get; set; }
        public string po_no { get; set; }
        public int? item_id { get; set; }
        public decimal? cost_inc_vat { get; set; }
        public string vat_flag { get; set; }
        public decimal? vat_rate { get; set; }
        public decimal? cost_vat { get; set; }
        public decimal? cost_exc_vat { get; set; }
        public decimal? cost_other_exc_vat { get; set; }
        public decimal? cost_repair_exc_vat { get; set; }
        public int? po_qty { get; set; }
        public int? receive_qty { get; set; }
        public int? log_id { get; set; }
        public decimal? cost_discount { get; set; }
        
    }
    
}