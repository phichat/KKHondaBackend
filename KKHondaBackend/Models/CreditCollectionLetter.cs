using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class CreditCollectionLetter
    {
        public int ClId { get; set; }
        public int ContractId { get; set; }
        public string CommunicateType { get; set; }
        public string AddressType { get; set; }
        public int BranchId { get; set; }
    }

    public partial class CreditCollectionLetterDetail
    {
        public int CldId { get; set; }
        public int ContractId { get; set; }
        public DateTime CldDate { get; set; }
        public string CldBookNo { get; set; }
        public string CldReferNo { get; set; }
        public string CldSubject { get; set; }
        public string CldExpenses { get; set; }
        public int CldStatus { get; set; }
        public int CldComeback { get; set; }
        public DateTime CldPaymentDate { get; set; }
        public int CldOperatorId { get; set; }
        public string CldTurnover { get; set; }
        public DateTime CldCompletDate { get; set; }
        public int CldStatusLock { get; set; }
        public string CldRemarkLock { get; set; }
    }

    public partial class CreditCollectionLetterGroup
    {
        public CreditCollectionLetter Head { get; set; }
        public CreditCollectionLetterDetail Detail { get; set; }
    }

    public partial class CreditCollectionLetterGroupList
    {
        public CreditCollectionLetter Head { get; set; }
        public List<CreditCollectionLetterDetail> Detail { get; set; }
    }

    public class CreditCollectionLetterList
    {
        public int ContractId { get; set; }
        public string ContractNo { get; set; }
        public string ContractDate { get; set; }
        public string ContractHire { get; set; }
        public string ContractGurantor1 { get; set; }
        public string ContractGurantor2 { get; set; }
        public string StatusDesc { get; set; }
        public string CommunicateType { get; set; }
        public string AddressType { get; set; }
    }

}
