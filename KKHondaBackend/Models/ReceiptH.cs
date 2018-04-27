using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class ReceiptH
    {
        public string BranchCode { get; set; }
        public string RefNo { get; set; }
        public string ReceiptNo { get; set; }
        public string ReceiptStatus { get; set; }
        public string SupplierCode { get; set; }
        public DateTime? DocumentDate { get; set; }
        public DateTime? ExpectedReceiptDate { get; set; }
        public string ConfirmReceiptBy { get; set; }
        public DateTime? ConfirmReceiptDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
