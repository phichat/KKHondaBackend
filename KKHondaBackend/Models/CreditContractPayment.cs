using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class CreditContractPayment
    {
        public int PaymentId { get; set; }
        public int ContractId { get; set; }
        public int InstalmentNo { get; set; }
        public decimal PayPrice { get; set; }
        public decimal PayVatPrice { get; set; }
        public decimal PayNetPrice { get; set; }
        public DateTime PayDate { get; set; }
        public int Payeer { get; set; }
        public string BankCode { get; set; }
        public int PaymentType { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? DiscountPrice { get; set; }
        public decimal? DistCutOffSaleRate { get; set; }
        public decimal? DistCutOffSalePrice { get; set; }
        public decimal Remain { get; set; }
        public decimal RemainVatPrice { get; set; }
        public decimal RemainNetPrice { get; set; }
        public int TaxInvoiceBranchId { get; set; }
        public string TaxInvoiceNo { get; set; }
        public string ReceiptNo { get; set; }
        public int? DelayDueDate { get; set; }
        public DateTime? CheckDueDate { get; set; }
        public decimal? FineSum { get; set; }
        public decimal? FineSumRemain { get; set; }
        public string FineSumCode { get; set; }
        // public int? FineSumStatus { get; set; }
        // public decimal? FineSumOther { get; set; }
        public string PaymentName { get; set; }
        public string DocumentRef { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}