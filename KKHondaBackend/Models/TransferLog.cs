using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class TransferLog
    {
        public int LogId { get; set; }
        public int? LogType { get; set; }
        public string TranferNo { get; set; }
        public string LogRef { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public int? LogItemType { get; set; }
        public int? ItemId { get; set; }
        public int? ModelId { get; set; }
        public int? ColorId { get; set; }
        public string EngineNo { get; set; }
        public string FrameNo { get; set; }
        public string PartCode { get; set; }
        public int? Qty { get; set; }
        public int? LogStatus { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int BQty { get; set; }
    }
}
