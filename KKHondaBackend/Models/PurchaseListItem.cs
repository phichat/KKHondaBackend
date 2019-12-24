using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{

    public partial class PurchaseListItem
    {
        public int po_id { get; set; }
        public string po_no { get; set; }
        public int? cat_id { get; set; }
        //public string cat_name { get; set; }
        public int? brand_id { get; set; }
        //public string brand_name { get; set; }
        public int? model_id { get; set; }
        //public string model_name { get; set; }
        public int? type_id { get; set; }
        //public string type_name { get; set; }
        public int? color_id { get; set; }
        //public string color_name { get; set; }
        public decimal? unit_price { get; set; }
        public decimal? unit_qty { get; set; }
        public int? create_id { get; set; }
        //public string create_name { get; set; }
        public DateTime? create_date { get; set; }
        public int? update_id { get; set; }
        //public string update_name { get; set; }
        public DateTime? update_date { get; set; }
    }

    public partial class PurchaseListItemRes
    {
        public int po_id { get; set; }
        public string po_no { get; set; }
        public int? cat_id { get; set; }
        public string cat_name { get; set; }
        public int? brand_id { get; set; }
        public string brand_name { get; set; }
        public int? model_id { get; set; }
        public string model_name { get; set; }
        public int? type_id { get; set; }
        public string type_name { get; set; }
        public int? color_id { get; set; }
        public string color_name { get; set; }
        public decimal? unit_price { get; set; }
        public decimal? unit_qty { get; set; }
        public int? create_id { get; set; }
        public string create_name { get; set; }
        public DateTime? create_date { get; set; }
        public int? update_id { get; set; }
        public string update_name { get; set; }
        public DateTime? update_date { get; set; }
    }

}