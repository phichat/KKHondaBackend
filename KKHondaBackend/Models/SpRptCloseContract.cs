using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public class SpRptCloseContract
    {
        public Int64 Id { get; set; }
        public string ContractNo { get; set; }
        public DateTime? ContractDate { get; set; }
        public int? ContractBranchId { get; set; }
        public string ContractBranchName { get; set; }
        public string ContractHire { get; set; }
        public string HireFullName { get; set; }
        public DateTime? EndContractDate { get; set; }
        public decimal? RemainTotal { get; set; }
        public int? ContractStatus { get; set; }
        public string ContractStatusDesc { get; set; }
        public DateTime? EndDueDate { get; set; }
        public int? KeeperBy { get; set; }
        public string KeeperByFullName { get; set; }
    }
}
