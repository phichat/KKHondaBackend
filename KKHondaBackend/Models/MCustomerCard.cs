using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MCustomerCard
    {
        public string CustomerCode { get; set; }
        public string CardType { get; set; }
        public string CardId { get; set; }
        public DateTime? CardIssueDate { get; set; }
        public DateTime? CardExpiryDate { get; set; }
        public string CardLocation { get; set; }
        public string CardPhoto { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public MCustomer CustomerCodeNavigation { get; set; }
    }
}
