using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKHondaBackend.Models
{
    public class SpDashboardBookingPaymentType
    {
        public Int64 Id { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public int? BookingPaymentType { get; set; }
        public string BookingPaymentTypeDesc { get; set; }
        public int? BookSellPrice { get; set; }
        public int? ItemAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
