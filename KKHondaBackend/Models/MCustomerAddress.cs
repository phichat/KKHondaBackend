using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MCustomerAddress
    {
        public string CustomerCode { get; set; }
        public string AddressType { get; set; }
        public string Address { get; set; }
        public string AmphorCode { get; set; }
        public string ProvinceCode { get; set; }
        public string Zipcode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Remarks { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public MCustomer CustomerCodeNavigation { get; set; }
    }
}
