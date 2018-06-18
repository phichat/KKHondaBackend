using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class CreditCalculate
    {
        public int CalculateId { get; set; }
        public int BookingId { get; set; }
        public decimal OutStandingPrice { get; set; }
        public decimal NetPrice { get; set; }
        public int SellTypeId { get; set; }
        public int SellAcitvityId { get; set; }
        public decimal Deposit { get; set; }
        public decimal DepositPrice { get; set; }
        public int InstalmentEnd { get; set; }
        public decimal InstalmentPrice { get; set; }
        public decimal InstalmentRemain { get; set; }
        public decimal Interest { get; set; }
        public decimal Remain { get; set; }
        public int TypePayment { get; set; }
        public DateTime FirstPayment { get; set; }
        public int DueDate { get; set; }
        public decimal? NowVat { get; set; }
        public decimal? PromotionalPrice { get; set; }
        public decimal? Irr { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
