using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class CarRegisListItemDoc
    {
        public int DocId { get; set; }
        public string BookingNo { get; set; }
        public string SendBackCode { get; set; }
        public bool IsReceive { get; set; }
        public DateTime ReceiveDate { get; set; }
        public int ReceiveBy { get; set; }
        public bool? IsSend { get; set; }
        public int? SendBy { get; set; }
        public DateTime? SendDate { get; set; }
        public string Remark { get; set; }
    }

    public partial class CarRegisListItemDocRes
    {
        public int DocId { get; set; }
        public string BookingNo { get; set; }
        public string SendBackCode { get; set; }
        public string SendBackName { get; set; }
        public bool? IsReceive { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public int? ReceiveBy { get; set; }
        public string ReceiveName { get; set; }
        public bool? IsSend { get; set; }
        public int? SendBy { get; set; }
        public string SendName { get; set; }
        public DateTime? SendDate { get; set; }
        public string Remark { get; set; }
    }
}

