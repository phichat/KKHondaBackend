using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class CarRegisList
    {
        public int BookingId { get; set; }
        public string BookingNo { get; set; }
        public string RevNo { get; set; }
        public int? Status1 { get; set; }
        public int? Status2 { get; set; }
        public DateTime BookingDate { get; set; }
        public int? State1 { get; set; }
        public int? State2 { get; set; }
        public string ENo { get; set; }
        public string FNo { get; set; }
        public decimal? Price1 { get; set; }
        public decimal? VatPrice1 { get; set; }
        public decimal? NetPrice1 { get; set; }
        public decimal? CutBalance { get; set; }
        public decimal? Price2 { get; set; }
        public decimal? Price3 { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? BranchId { get; set; }
        public DateTime? TransportReceiptDate { get; set; }
        public double? TransportServiceCharge { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
    }

    public partial class CarRegisListRes
    {
        public int BookingId { get; set; }
        public string BookingNo { get; set; }
        public int? Status1 { get; set; }
        public int? Status2 { get; set; }
        public string Status1Desc { get; set; }
        public string Status2Desc { get; set; }
        public DateTime BookingDate { get; set; }
        public int? State1 { get; set; }
        public int? State2 { get; set; }
        public string ENo { get; set; }
        public string FNo { get; set; }
        public decimal? Price1 { get; set; }
        public decimal? VatPrice1 { get; set; }
        public decimal? NetPrice1 { get; set; }
        public decimal? CutBalance { get; set; }
        public decimal? Price2 { get; set; }
        public decimal? Price3 { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? CreateBy { get; set; }
        public string CreateName { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public string UpdateName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? BranchId { get; set; }
        public DateTime? TransportReceiptDate { get; set; }
        public double? TransportServiceCharge { get; set; }
        public string BranchName { get; set; }
        public string BranchProvince { get; set; }
        public string TagNo { get; set; }
        public string Province { get; set; }
        public DateTime? TagRegis { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
    }

    public partial class CarRegisWaitingTagRes
    {
        public int? BookingPaymentType { get; set; }
        public string PaymentTypeDesc { get; set; }
        public string SellNo { get; set; }
        public string BookingNo { get; set; }
        public string CusSellName { get; set; }
        public string BookTitleName { get; set; }
        public string BookFName { get; set; }
        public string BookSName { get; set; }
        public string BookIdCard { get; set; }
        public string BookContactNo { get; set; }
        public int? FreeAct { get; set; }
        public int? FreeTag { get; set; }
        public int? FreeWarranty { get; set; }
        public decimal? BookNetPrice { get; set; }
        public DateTime? SellDate { get; set; }
        public int? SellBy { get; set; }
        public string SellName { get; set; }
        public string RegisName { get; set; }
        public string ENo { get; set; }
        public string FNo { get; set; }
        public int? FiId { get; set; }
    }

    public partial class CreateConFormBody
    {
        public CarRegisList TagRegis { get; set; }
        public CarHistory TagHistory { get; set; }
        public List<CarRegisListItem> TagListItem { get; set; }
    }
    public partial class UpdateConFormBody
    {
        public CarRegisList TagRegis { get; set; }
        public CarHistory TagHistory { get; set; }
        public List<CarRegisListItem> TagListItem { get; set; }
        public List<CarRegisListItem> TrashTagListItem { get; set; }
    }
    public partial class CarExcept
    {
        public string Car { get; set; }
    }

    public partial class CarRegisListCancel
    {
        public string BookingNo { get; set; }
        public string Reason { get; set; }
        public int UpdateBy { get; set; }
    }

    public partial class CarRegisItemRes
    {
        public IEnumerable<CarRegisListItemRes> CarRegisListItemRes { get; set; }
        public IEnumerable<CarRegisListItemDocRes> CarRegisListItemDocRes { get; set; }
    }
}