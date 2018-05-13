using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class CreditContract
    {
        public int ContractId { get; set; }
        public int BookingId { get; set; }
        public int CalculateId { get; set; }
        public string ContractNo { get; set; }
        public string ContractType { get; set; }
        public DateTime ContractDate { get; set; }
        public int AreaPayment { get; set; }
        public int ContractPoint { get; set; }
        public int ContractGroup { get; set; }
        public int ContractHire { get; set; }
        public int? ContractUser { get; set; }
        public int ContractGurantor1 { get; set; }
        public int? ContractGurantor2 { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CheckerBy { get; set; }
        public int? ApproveBy { get; set; }
        public int? KeeperBy { get; set; }
        public int ContractStatus { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
