using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
  public partial class CreditContract
  {
    public int ContractId { get; set; }
    public int BookingId { get; set; }
    public int? BranchId { get; set; }
    public int SaleId { get; set; }
    public string ContractNo { get; set; }
    public DateTime? ContractDate { get; set; }
    public int? AreaPayment { get; set; }
    public int? ContractPoint { get; set; }
    public string ContractType { get; set; }
    public string ContractGroup { get; set; }
    public string ContractHire { get; set; }
    public string HireAddress { get; set; }
    public string HireProvinceCode { get; set; }
    public string HireAmpherCode { get; set; }
    public string HireZipCode { get; set; }
    public string ContractOwner { get; set; }
    public string OwnerTaxNo { get; set; }
    public string OwnerAddress { get; set; }
    public string OwnerProvinceCode { get; set; }
    public string OwnerAmpherCode { get; set; }
    public string OwnerZipCode { get; set; }
    public string ContractMate { get; set; }
    public string ContractBooking { get; set; }
    public string ContractGurantor1 { get; set; }
    public string GurantorRelation1 { get; set; }
    public string ContractGurantor2 { get; set; }
    public string GurantorRelation2 { get; set; }
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

  public class SearchContract
  {
    public int? Status { get; set; }
    public string ContractNo { get; set; }
    public DateTime? ContractDate { get; set; }
    public string HireName { get; set; }
    public string HireIdCard { get; set; }
    public string ENo { get; set; }
    public string FNo { get; set; }
    public int? ContractGroup { get; set; }
    public int? ContractType { get; set; }
    public int? ContractPoint { get; set; }
    public int BranchId { get; set; }
  }

  
}
