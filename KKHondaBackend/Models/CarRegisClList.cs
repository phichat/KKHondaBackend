using System;
namespace KKHondaBackend.Models
{
    public partial class CarRegisClList
    {
        public int ClId { get; set; }
        public string ClNo { get; set; }
        public string AlNo { get; set; }
        public string RevNo { get; set; }
        public int RefundId { get; set; }
        public decimal BalancePrice { get; set; }
        public decimal ReceivePrice { get; set; }
        public decimal NetPrice { get; set; }
        public string BankCode { get; set; }
        public string DocumentRef { get; set; }
        public int PaymentType { get; set; }
        public int BranchId { get; set; }
        public int? Status { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }
        public string Remark { get; set; }
    }

    public partial class CarRegisClListRes
    {
        public int ClId { get; set; }
        public string ClNo { get; set; }
        public string AlNo { get; set; }
        public int RefundId { get; set; }
        public string RefundName { get; set; }
        public decimal BalancePrice { get; set; }
        public decimal ReceivePrice { get; set; }
        public decimal NetPrice { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string DocumentRef { get; set; }
        public int PaymentType { get; set; }
        public string PaymentDesc { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int? Status { get; set; }
        public string StatusDesc { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public string CreateName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }
        public string UpdateName { get; set; }
        public string Remark { get; set; }
    }

    public class CarRegisClSummary
    {
        public string AlNo { get; set; }
        public decimal BalancePrice { get; set; }
        public decimal ReceivePrice { get; set; }
        public decimal NetPrice { get; set; }

    }
}