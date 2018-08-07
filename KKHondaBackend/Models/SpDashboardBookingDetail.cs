using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKHondaBackend.Models
{
    public class SpDashboardBookingDetail
    {
        public Int64 Id { get; set; }
        public DateTime? BookingDate { get; set; }
        public string BookingNo { get; set; }
        public string BookingFullName { get; set; }
        public string BookingIdCard { get; set; }
        public string BranchName { get; set; }
        public decimal? BookSellPrice { get; set; }
        public string BookingPaymentTypeDesc { get; set; }
        public string BookingStatus { get; set; }
        public string CancelRemark { get; set; }
        public string BrandName { get; set; }
        public string TypeName { get; set; }
        public string ModelCode { get; set; }
        public string ModelName { get; set; }
        public string ColorName { get; set; }
    }
}
