using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class TransferH
    {
        public string TransferNo { get; set; }
        public string TransferStatus { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string TransferDesc { get; set; }
        public string RefNo { get; set; }
        public string BranchCodeOut { get; set; }
        public string TransferOutBy { get; set; }
        public DateTime? TranferOutDate { get; set; }
        public string BranchCodeIn { get; set; }
        public string TransferInBy { get; set; }
        public DateTime? TransferInDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
