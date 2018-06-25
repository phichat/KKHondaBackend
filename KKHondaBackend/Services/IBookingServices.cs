﻿using System;
namespace KKHondaBackend.Services
{
    public interface IBookingServices
    {
        Booking GetBookingById(int id);

        BookingList[] GetBookingLists();

    }

    public class Booking{
        public string Address {get; set; }
        public string ContractNo {get; set; }
        public decimal? Deposit {get; set; }
        public decimal? DistcountB {get; set; }
        public decimal? DistcountP {get; set; }
        public decimal? DistcountPPrice {get; set; }
        public string Email {get; set; }
        public string CustCode {get; set; }
        public string CustFullName {get; set; }
        public int? GenderCode {get; set; }
        public string GenderName {get; set; }
        public string IdCard {get; set; }
        public string BookingDate {get; set; }
        public int? DepositType {get; set; }
        public int? PookingId {get; set; }
        public string PookingNo {get; set; }
        public int? BookingId { get; set; }
        public string BookingNo { get; set; }
        public int? PtymentType {get; set; }
        public int? BookingStatus {get; set; }
        public int? BookingType {get; set; }
        public decimal? NetPrice {get; set; }
        public string NickName {get; set; }
        public decimal? OutStandingPrice {get; set; }
        public string ReceiveDate {get; set; }
        public string Remark {get; set; }
        public decimal? SellPrice {get; set; }
        public decimal? TotalDiscount {get; set; }
        public decimal? Vat {get; set; }
        public decimal? VatPrice {get; set; }
        public int? FreeAct {get; set; }
        public int? FreeTag {get; set; }
        public int? FreeWarranty {get; set; }
        public int? BranchId { get; set; }
        public int? CreateBy { get; set; }
        public BookingItem[] BookingItem { get; set; }
    }

    public class BookingItem {
        public int? BookingId { get; set; }
        public string BrandName { get; set; }
        public int? CatId { get; set; }
        public string CatName { get; set; }
        public string ClassName { get; set; }
        public string ColorName { get; set; }
        public decimal? CostNet { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? CostVat { get; set; }
        public decimal? CostVatPrice { get; set; }
        public int? ItemDetailType { get; set; }
        public int? ItemId { get; set; }
        public decimal? ItemQty { get; set; }
        public decimal? ItemType { get; set; }
        public string ModelCode { get; set; }
        public string ModelName { get; set; }
        public string PartClass { get; set; }
        public string PartCode { get; set; }
        public string PartName { get; set; }
        public string PartSource { get; set; }
        public string PartSpareCode { get; set; }
        public decimal? RealDiscountB { get; set; }
        public decimal? RealDiscountP { get; set; }
        public decimal? RealVat { get; set; }
        public decimal? RealNetPrice { get; set; }
        public decimal? RealVatPrice { get; set; }
        public decimal? RealSellPrice { get; set; }
        public decimal? RealTotalDiscount { get; set; }
        public decimal? RealDiscountPPrice { get; set; }
        public int? RunId { get; set; }
        public decimal? SellNet { get; set; }
        public decimal? SellVat { get; set; }
        public decimal? SellPrice { get; set; }
        public decimal? SellVatPrice { get; set; }
        public string TypeName { get; set; }
        public string UnitName { get; set; }
    }

    public class BookingList {
        public int BookingId {get; set; }
        public string BookingNo {get; set; }
        public string Status {get; set; }
        public string PaymentType {get; set; }
        public string DepositType {get; set; }
        public string BookingDate {get; set; }
        public string ReceiveDate {get; set; }
        public string CustFullName {get; set; }
        public string IdCard {get; set; }
        public string ContractNo {get; set; }
        public string Email {get; set; }
        public string Address {get; set; }
        public string ModelCode {get; set; }
        public string ModelName {get; set; }
        public string ColorName {get; set; }
        public decimal? NetPrice {get; set; }
        public decimal? Deposit {get; set; }
        public decimal? OutStandingPrice {get; set; }
        public DateTime? CreateDate {get; set; }
        public string CreateBy {get; set; }
    }
}