using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class PurchaseHead
    {
        public int id { get; set; }
        public string po_no { get; set; }
        public int branch_id { get; set; }
        public string dealer_code { get; set; }
        public int po_type { get; set; }
        public int po_status { get; set; }
        public DateTime? po_date { get; set; }
        public DateTime? delivery_date { get; set; }
        public string contact_name { get; set; }
        public string contact_phone { get; set; }
        public string contact_fax { get; set; }
        public string po_remark { get; set; }
        public int? create_id { get; set; }
        public DateTime? create_date { get; set; }
        public int? update_id { get; set; }
        public DateTime? update_date { get; set; }
        
    }    

}