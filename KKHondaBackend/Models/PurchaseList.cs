using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class PurchaseList
    {
        public int po_id { get; set; }
        public string po_no { get; set; }
        public int status { get; set; }
        //public string status_desc { get; set; }
        public DateTime? po_date { get; set; }
        public DateTime? due_date { get; set; }
        public int supplier_id { get; set; }
        //public string supplier_name { get; set; }
        public string remark { get; set; }

        public int? create_id { get; set; }
        //public string create_name { get; set; }
        public DateTime? create_date { get; set; }
        public int? update_id { get; set; }
        //public string update_name { get; set; }
        public DateTime? update_date { get; set; }

        public string cash_flag { get; set; }
        public decimal? cash_price { get; set; }

        public string cheque_flag { get; set; }
        public int? cheque_bank_id { get; set; }
        public string cheque_branch { get; set; }
        public string cheque_no { get; set; }
        public DateTime? cheque_date { get; set; }
        public decimal? cheque_price { get; set; }

        public decimal? total_price { get; set; }
        public string vat_flag { get; set; }
        public decimal? total_vat { get; set; }
        public decimal? total_vat_price { get; set; }
        public decimal? total_net_price { get; set; }       

    }

    public partial class PurchaseListRes
    {
        public int po_id { get; set; }
        public string po_no { get; set; }
        public int? status { get; set; }
        public string status_desc { get; set; }
        public DateTime? po_date { get; set; }
        public DateTime? due_date { get; set; }
        public int supplier_id { get; set; }
        public string supplier_name { get; set; }
        public string remark { get; set; }

        public int? create_id { get; set; }
        public string create_name { get; set; }
        public DateTime? create_date { get; set; }
        public int? update_id { get; set; }
        public string update_name { get; set; }
        public DateTime? update_date { get; set; }

        public string cash_flag { get; set; }
        public decimal? cash_price { get; set; }

        public string cheque_flag { get; set; }
        public int? cheque_bank_id { get; set; }
        public string cheque_branch { get; set; }
        public string cheque_no { get; set; }
        public DateTime? cheque_date { get; set; }
        public decimal? cheque_price { get; set; }

        public decimal? total_price { get; set; }
        public string vat_flag { get; set; }
        public decimal? total_vat { get; set; }
        public decimal? total_vat_price { get; set; }
        public decimal? total_net_price { get; set; }

    }

    public partial class PurchaseListDetailRes
    {
        public int po_id { get; set; }
        public string po_no { get; set; }
        public int? status { get; set; }
        public string status_desc { get; set; }
        public DateTime? po_date { get; set; }
        public DateTime? due_date { get; set; }
        public int supplier_id { get; set; }
        public string supplier_name { get; set; }
        public string remark { get; set; }

        public string cash_flag { get; set; }
        public decimal? cash_price { get; set; }

        public string cheque_flag { get; set; }
        public int? cheque_bank_id { get; set; }
        public string cheque_branch { get; set; }
        public string cheque_no { get; set; }
        public DateTime? cheque_date { get; set; }
        public decimal? cheque_price { get; set; }

        public decimal? total_price { get; set; }
        public string vat_flag { get; set; }
        public decimal? total_vat { get; set; }
        public decimal? total_vat_price { get; set; }
        public decimal? total_net_price { get; set; }

        public int? create_id { get; set; }
        public string create_name { get; set; }
        public DateTime? create_date { get; set; }
        public int? update_id { get; set; }
        public string update_name { get; set; }
        public DateTime? update_date { get; set; }

        public List<PurchaseListItemRes> detail { get; set; }

    }

}