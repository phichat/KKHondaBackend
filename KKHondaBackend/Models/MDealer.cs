using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MDealer
    {

        public int id { get; set; }
        public string dealer_id { get; set; }
        public string dealer_code { get; set; }
        public string prename_code { get; set; }
        public string dealer_name_th { get; set; }
        public string dealer_name_en { get; set; }
        public string dealer_contact { get; set; }
        public string dealer_address { get; set; }
        public string amphor_code { get; set; }
        public string province_code { get; set; }
        public string zipcode { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string taxid { get; set; }
        public DateTime? issue_date { get; set; }
        public string issue_by { get; set; }
        public DateTime? expiry_date { get; set; }
        public decimal total_deposit { get; set; }
        public string branch_hq { get; set; }
        public string branch_code { get; set; }
        public string branch_name { get; set; }
        public string bank_no { get; set; }
        public string bank_type { get; set; }
        public string bank_name { get; set; }
        public int? bank_id { get; set; }
        public string bank_code { get; set; }
        public string bank_branch_code { get; set; }
    }
}
