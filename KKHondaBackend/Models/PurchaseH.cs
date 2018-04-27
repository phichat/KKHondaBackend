using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class PurchaseH
    {
        public string PurchaseNo { get; set; }
        public string RefNo { get; set; }
        public string BranchCode { get; set; }
        public string PurchaseType { get; set; }
        public string PurchaseStatus { get; set; }
        public DateTime? DocumentDate { get; set; }
        public DateTime? ExpectedReceiptDate { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierPrename { get; set; }
        public string SupplierName { get; set; }
        public string SupplierSurname { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
