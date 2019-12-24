using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class ReceiveH
    {
        public int id { get; set; }
        public string receive_no { get; set; }
        public int? receive_id { get; set; }
        public DateTime? receive_date { get; set; }
        public int? receive_status { get; set; }
        public int? receive_type { get; set; }
        public string dealer_code { get; set; }
        public string purchase_no { get; set; }
        public string remark { get; set; }
        public int? create_id { get; set; }
        public DateTime? create_date { get; set; }
        public int? update_id { get; set; }
        public DateTime? update_date { get; set; }

        public string transfer_code { get; set; }
        public string delivery_code { get; set; }
        public DateTime? delivery_date { get; set; }

    }

    public partial class ReceiveHRes
    {
        public int id { get; set; }
        public string receive_no { get; set; }
        public int? receive_id { get; set; }
        public string receive_name { get; set; }
        public DateTime? receive_date { get; set; }
        public int? receive_status { get; set; }
        public string receive_status_name { get; set; }
        public int? receive_type { get; set; }
        public string receive_type_name { get; set; }
        public string dealer_code { get; set; }
        public string dealer_name { get; set; }
        public string purchase_no { get; set; }
        public string remark { get; set; }
        public int? create_id { get; set; }
        public string create_name { get; set; }
        public DateTime? create_date { get; set; }
        public int? update_id { get; set; }
        public string update_name { get; set; }
        public DateTime? update_date { get; set; }

        public string transfer_code { get; set; }
        public string delivery_code { get; set; }
        public DateTime? delivery_date { get; set; }
    }

    public partial class ReceiveDetailRes
    {
        public int id { get; set; }
        public string receive_no { get; set; }
        public int? receive_id { get; set; }
        public string receive_name { get; set; }
        public DateTime? receive_date { get; set; }
        public int? receive_status { get; set; }
        public string receive_status_name { get; set; }
        public int? receive_type { get; set; }
        public string receive_type_name { get; set; }
        public string dealer_code { get; set; }
        public string dealer_name { get; set; }
        public string purchase_no { get; set; }
        public string remark { get; set; }
        public int? create_id { get; set; }
        public string create_name { get; set; }
        public DateTime? create_date { get; set; }
        public int? update_id { get; set; }
        public string update_name { get; set; }
        public DateTime? update_date { get; set; }

        public string transfer_code { get; set; }
        public string delivery_code { get; set; }
        public DateTime? delivery_date { get; set; }

        public List<ReceiveDRes> detail { get; set; }
    }

    public partial class ListDealer
    {
        public int Id { get; set; }
        public string Desc { get; set; }
    }

    public partial class ListStatus
    {
        public int Id { get; set; }
        public string Desc { get; set; }
    }

    public partial class ListLineStatus
    {
        public int Id { get; set; }
        public string Desc { get; set; }
    }

    public partial class ListType
    {
        public int Id { get; set; }
        public string Desc { get; set; }
    }

}