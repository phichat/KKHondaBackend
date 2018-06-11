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
        public int? DiscountType { get; set; }
        public int? BranchId { get; set; }
        public string CancelRemark { get; set; }
        public int? ReturnDepostit { get; set; }
        public decimal? ReturnDepositPrice { get; set; }
        public DateTime? CancelDate { get; set; }
        public int? CancelBy { get; set; }
        public string BookBirthDate { get; set; }
        public decimal? PaymentPrice { get; set; }
        public int? PaymentType { get; set; }
        public string CusSellName { get; set; }
        public string CusTaxNo { get; set; }
        public string CusTaxBranch { get; set; }
        public string SellRemark { get; set; }
        public DateTime? SellDate { get; set; }
        public int? SellBy { get; set; }
        public string LStartDate { get; set; }
        public int? LPayDay { get; set; }
        public int? LTerm { get; set; }
        public int? FiId { get; set; }
        public decimal? LInterest { get; set; }
        public decimal? LComPrice { get; set; }
        public string SellNo { get; set; }
        public decimal? LPriceTerm { get; set; }
        public string VatNo { get; set; }
        public DateTime? VatDate { get; set; }
        public int? VatBy { get; set; }
    }
}
