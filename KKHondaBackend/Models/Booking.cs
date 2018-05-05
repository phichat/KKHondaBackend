using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class Booking
    {
        public int BookingId { get; set; }
        public string BookingNo { get; set; }
        public int? BookingStatus { get; set; }
        public int? BookingType { get; set; }
        public int? BookingPaymentType { get; set; }
        public int? BookingDepositType { get; set; }
        public string BookingDate { get; set; }
        public string BookReceiveDate { get; set; }
        public string CustomerCode { get; set; }
        public string BookTitleName { get; set; }
        public int? BookGender { get; set; }
        public string BookFName { get; set; }
        public string BookSName { get; set; }
        public string BookNickName { get; set; }
        public string BookIdCard { get; set; }
        public string BookContactNo { get; set; }
        public string BookEmail { get; set; }
        public string BookAddress { get; set; }
        public decimal? BookSellPrice { get; set; }
        public decimal? BookDiscountB { get; set; }
        public decimal? BookDiscountP { get; set; }
        public decimal? BookDiscountPPrice { get; set; }
        public decimal? BookTotalDiscount { get; set; }
        public decimal? BookVat { get; set; }
        public decimal? BookVatPrice { get; set; }
        public decimal? BookNetPrice { get; set; }
        public decimal? BookDeposit { get; set; }
        public decimal? BookOutstandingPrice { get; set; }
        public int? FreeAct { get; set; }
        public int? FreeTag { get; set; }
        public int? FreeWarranty { get; set; }
        public string BookRemark { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
