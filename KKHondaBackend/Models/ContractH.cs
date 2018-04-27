using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class ContractH
    {
        public string BranchCode { get; set; }
        public string ContractNo { get; set; }
        public string SaleNo { get; set; }
        public DateTime? ContractDate { get; set; }
        public TimeSpan? ContractTime { get; set; }
        public string GroupCode { get; set; }
        public string TypeCode { get; set; }
        public string CategoryCode { get; set; }
        public string BrandCode { get; set; }
        public string ModelCode { get; set; }
        public string ColorCode { get; set; }
        public string EngineNo { get; set; }
        public string FrameNo { get; set; }
        public string SaleTypesId { get; set; }
        public string SaleActivityId { get; set; }
        public decimal? TotalNetPrice { get; set; }
        public decimal? DepositRate { get; set; }
        public decimal? DepositAmt { get; set; }
        public int? InstalmentEnd { get; set; }
        public decimal? InstalmentRate { get; set; }
        public decimal? InstalmentAmt { get; set; }
        public decimal? RemainAmt { get; set; }
        public DateTime? FirstPayment { get; set; }
        public int? DueDate { get; set; }
        public decimal? VatRate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string ReserveNo { get; set; }
        public string GuarantorCode { get; set; }
        public string GuarantorPrename { get; set; }
        public string GuarantorName { get; set; }
        public string GuarantorSurname { get; set; }
        public string ContractStatus { get; set; }
        public string GuarantorPhone { get; set; }
    }
}
