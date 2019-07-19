using System;
namespace KKHondaBackend.Models
{
    public partial class CarRegisList
    {
        public int BookingId { get; set; }
        public string BookingNo { get; set; }
        public int BookingStatus { get; set; }
        public string ENo { get; set; }
        public string FNo { get; set; }
        public decimal? Price1 { get; set; }
        public decimal? Price2 { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? BranchId { get; set; }
        public DateTime? TransportReceiptDate { get; set; }
        public double? TransportServiceCharge { get; set; }
    }
}