using System;
namespace KKHondaBackend.Models
{
    public class BookingReasonCode
    {
        public int CancelId { get; set; }
        public string CancelCode { get; set; }
        public string CancelDep { get; set; }
        public int CreateBy { get; set; }
        
        public DateTime CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Status { get; set; }
    }
}

