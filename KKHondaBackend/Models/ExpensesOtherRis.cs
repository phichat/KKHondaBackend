
using System;

namespace KKHondaBackend.Models
{
    public partial class ExpensesOtherRis
    {
        public int ExpensesID { get; set; }
        public string ExpensesCode { get; set; }
        public string ExpensesDescription { get; set; }
        public decimal ExpensesAmount { get; set; }
        public int ExpensesType { get; set; }
        public bool Status { get; set; }
        public int CreateBy { get; set; }
        public DateTime DateCreate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? DateUpdate { get; set; }
    }

    public partial class ExpensesOtherRisRes
    {
        public int ExpensesID { get; set; }
        public string ExpensesCode { get; set; }
        public string ExpensesDescription { get; set; }
        public decimal ExpensesAmount { get; set; }
        public int ExpensesType { get; set; }
        public string ExpensesTypeDesc { get; set; }
        public bool Status { get; set; }
        public string StatusDesc { get; set; }
        public int CreateBy { get; set; }
        public string CreateName { get; set; }
        public DateTime DateCreate { get; set; }
        public int? UpdateBy { get; set; }
        public string UpdateName { get; set; }
        public DateTime? DateUpdate { get; set; }
    }
}