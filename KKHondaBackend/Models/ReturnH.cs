using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class ReturnH
    {
        public int id { get; set; }
        public string return_no { get; set; }
        public int? return_type { get; set; }
        public string dealer_code { get; set; }
        public string receive_no { get; set; }
        public DateTime? receive_date { get; set; }        
        public string remark { get; set; }
        public int? create_id { get; set; }
        public DateTime? create_date { get; set; }
        public int? update_id { get; set; }
        public DateTime? update_date { get; set; }
        public int? return_status { get; set; }
        public DateTime? return_date { get; set; }
        

    }
    

}