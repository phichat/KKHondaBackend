using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MCustomer
    {
        public MCustomer()
        {
            MCustomerAddress = new HashSet<MCustomerAddress>();
            MCustomerCard = new HashSet<MCustomerCard>();
        }

        public string CustomerCode { get; set; }
        public string CustomerPrename { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerNickname { get; set; }
        public string CustomerLevel { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerSex { get; set; }
        public DateTime? Birthday { get; set; }
        public string Nationality { get; set; }
        public string Occupation { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string TypePersonal { get; set; }
        public string TypeCorporate { get; set; }
        public string TypeDealer { get; set; }
        public string TypeSupplier { get; set; }
        public string TypeOther { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public MCustomerLevel CustomerLevelNavigation { get; set; }
        public ICollection<MCustomerAddress> MCustomerAddress { get; set; }
        public ICollection<MCustomerCard> MCustomerCard { get; set; }
    }
}
