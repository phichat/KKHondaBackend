using System;
namespace KKHondaBackend.Models
{
    public class CreditTransaction
    {
        public int TransactionId { get; set; }
        public int ContractItemId { get; set; }
        public string Description { get; set; }
        public decimal? PayPrice { get; set; }
        public decimal? PayVatPrice { get; set; }
        public decimal? PayNetPrice { get; set; }
    }
}
