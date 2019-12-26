using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class ReceiveD
    {
        public int id { get; set; }
        public string receive_no { get; set; }
        public string dealer_no { get; set; }
        public int? cat_id { get; set; }
        public int? brand_id { get; set; }
        public int? model_id { get; set; }
        public int? type_id { get; set; }
        public int? color_id { get; set; }
        public string frame_no { get; set; }
        public string engine_no { get; set; }
        public string delivery_no { get; set; }
        public DateTime? delivery_date { get; set; }
        public string invoice_no { get; set; }
        public string tax_invoice_no { get; set; }
        public int? create_id { get; set; }
        public DateTime? create_date { get; set; }
        public int? update_id { get; set; }
        public DateTime? update_date { get; set; }
        public string license_no { get; set; }
        public int? branch_id { get; set; }
        public string line_remark { get; set; }
        public int? line_status { get; set; }      
        public decimal? cost_inc_vat { get; set; }
        public string vat_flag { get; set; }
        public decimal? vat_rate { get; set; }
        public decimal? cost_vat { get; set; }
        public decimal? cost_exc_vat { get; set; }
        public decimal? cost_other_exc_vat { get; set; }
        public decimal? cost_repair_exc_vat { get; set; }
        public int? whl_id { get; set; }
        public int? log_id { get; set; }
        public int? item_id { get; set; }
    }

    public partial class ReceiveDRes
    {
        public int id { get; set; }
        public string receive_no { get; set; }
        public string dealer_no { get; set; }
        public int? cat_id { get; set; }
        public string cat_code { get; set; }
        public int? brand_id { get; set; }
        public string brand_code { get; set; }
        public int? model_id { get; set; }
        public string model_code { get; set; }
        public int? type_id { get; set; }
        public string type_code { get; set; }
        public int? color_id { get; set; }
        public string color_code { get; set; }
        public string frame_no { get; set; }
        public string engine_no { get; set; }
        public string delivery_no { get; set; }
        public DateTime? delivery_date { get; set; }
        public string invoice_no { get; set; }
        public string tax_invoice_no { get; set; }

        public int? create_id { get; set; }
        public string create_name { get; set; }
        public DateTime? create_date { get; set; }
        public int? update_id { get; set; }
        public string update_name { get; set; }
        public DateTime? update_date { get; set; }

        public string license_no { get; set; }
        public int? branch_id { get; set; }
        public string branch_code { get; set; }
        public string line_remark { get; set; }
        public int? line_status { get; set; }
        public string line_status_name { get; set; }
        public decimal? cost_inc_vat { get; set; }
        public string vat_flag { get; set; }
        public decimal? vat_rate { get; set; }
        public decimal? cost_vat { get; set; }
        public decimal? cost_exc_vat { get; set; }
        public decimal? cost_other_exc_vat { get; set; }
        public decimal? cost_repair_exc_vat { get; set; }
        public int? whl_id { get; set; }
        public string whl_code { get; set; }
        public int? log_id { get; set; }
        public int? item_id { get; set; }
    }


    public partial class DOC_Receive
    {
        public string dealer_no { get; set; }
        public DateTime? delivery_date { get; set; }
        public int? cat_id { get; set; }
        public int? brand_id { get; set; }
        public int? model_id { get; set; }
        public int? type_id { get; set; }
        public int? color_id { get; set; }
        public string frame_no { get; set; }
        public string engine_no { get; set; }
        public string delivery_no { get; set; }
        public string invoice_no { get; set; }
        public string tax_invoice_no { get; set; }

        public decimal? cost_inc_vat { get; set; }
        public string vat_flag { get; set; }
        public decimal? vat_rate { get; set; }
        public decimal? cost_vat { get; set; }
        public decimal? cost_exc_vat { get; set; }
        public decimal? cost_other_exc_vat { get; set; }
        public decimal? cost_repair_exc_vat { get; set; }


    }

}