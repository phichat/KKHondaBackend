using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class CreditContract
    {
        public int ContractId { get; set; }
        public int BookingId { get; set; }
        public int? BranchId { get; set; }
        public int CalculateId { get; set; }
        public string ContractNo { get; set; }
        public DateTime? ContractDate { get; set; }
        public int? AreaPayment { get; set; }
        public int? ContractPoint { get; set; }
        public int? ContractType { get; set; }
        public int? ContractGroup { get; set; }
        public string ContractHire { get; set; }
        public string ContractMate { get; set; }
        public string ContractBooking { get; set; }
        public string ContractGurantor1 { get; set; }
        public int? GurantorRelation1 { get; set; }
        public string ContractGurantor2 { get; set; }
        public int? GurantorRelation2 { get; set; }
        public int? CreatedBy { get; set; }
        public int? CheckedBy { get; set; }
        public int? ApprovedBy { get; set; }
        public int? KeeperBy { get; set; }
        public int? ContractStatus { get; set; }
        public string RefNo { get; set; }
        public string Remark { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public DateTime? EndContractDate { get; set; }

    }
}
