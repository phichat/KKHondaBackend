using System;
namespace KKHondaBackend.Models
{
    public partial class CarRegisListItem
    {
        public int RunId { get; set; }
        public int? BookingId { get; set; }
        public int? ItemName { get; set; }
        public decimal? ItemPrice1 { get; set; }
        public decimal? ItemPrice2 { get; set; }
        public decimal? ItemPriceTotal { get; set; }
    }
}