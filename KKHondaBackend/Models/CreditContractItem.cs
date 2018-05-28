using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class CreditContractItem
    {
        public int ContractItemId { get; set; }
        public int ContractId { get; set; }
        public int ContractBranchId { get; set; }
        public int InstalmentNo { get; set; }
        public DateTime DueDate { get; set; }
        public decimal? VatRate { get; set; }
        public decimal? Balance { get; set; }
        public decimal? BalanceVatPrice { get; set; }
        public decimal? BalanceNetPrice { get; set; }
        public decimal? PayPrice { get; set; }
        public decimal? PayVatPrice { get; set; }
        public decimal? PayNetPrice { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? DiscountPrice { get; set; }
        public decimal? FineSum { get; set; }
        public int? TaxInvoiceBranchId { get; set; }
        public string TaxInvoiceNo { get; set; }
        public decimal? NetInvoice { get; set; }
        public int? Status { get; set; }
        public decimal? InterestInstalment { get; set; }
        public decimal? InterestRemainAccount { get; set; }
        public decimal? GoodsPriceRemain { get; set; }
        public decimal? InstalmentPrice { get; set; }
        public decimal? Remain { get; set; }
        public decimal? RemainVatPrice { get; set; }
        public decimal? RemainNetPrice { get; set; }
        public int? DelayDueDate { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
