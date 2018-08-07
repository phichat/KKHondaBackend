using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKHondaBackend.Models
{
    public class SpDashboardBookingType
    {
        public Int64 Id { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public int? BookingType { get; set; }
        public string BookingTypeDesc { get; set; }
        public int? BookingSellPrice { get; set; }
        public int? ItemAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
