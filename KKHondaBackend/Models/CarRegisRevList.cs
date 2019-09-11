using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public class CarRegisRevList
    {
        public int RevId { get; set; }
        public string RevNo { get; set; }
        public string SedNo { get; set; }
        public int BranchId { get; set; }
        public decimal TotalPrice1 { get; set; }
        public decimal TotalVatPrice1 { get; set; }
        public decimal TotalNetPrice { get; set; }
        public decimal TotalCutBalance { get; set; }
        public decimal TotalPrice2 { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalClBalancePrice { get; set; }
        public decimal TotalClReceivePrice { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalAccruedExpense { get; set; }
        public string Remark { get; set; }
        public string Reason { get; set; }
        public int Status { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class CarRegisRevListRes
    {
        public int RevId { get; set; }
        public string RevNo { get; set; }
        public string SedNo { get; set; }
        public string SedCreateName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public decimal TotalPrice1 { get; set; }
        public decimal TotalVatPrice1 { get; set; }
        public decimal TotalNetPrice { get; set; }
        public decimal TotalCutBalance { get; set; }
        public decimal TotalPrice2 { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalClBalancePrice { get; set; }
        public decimal TotalClReceivePrice { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalAccruedExpense { get; set; }
        public string Remark { get; set; }
        public string Reason { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public int CreateBy { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public string UpdateName { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class CarRegisRevListFormBody
    {
        public CarRegisRevList TagRev { get; set; }
        public List<CarRegisList> TagConList { get; set; }
        public List<CarRegisListItem> TagConListItem { get; set; }
        public List<CarRegisListItemDoc> TagListItemDoc { get; set; }
    }
}
