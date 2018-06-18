using System;
namespace KKHondaBackend.Services
{
    public interface ICustomerServices
    {
        Dropdown[] GetDropdownByKey(string term);

        Dropdown[] GetDropdowns();

        Customer GetCustomerByCode(string custCode);
    }

    public class Customer
    {
        public string CustomerCode { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerSex { get; set; }
        public string CustomerNickName { get; set; }
        public string CardType { get; set; }
        public string IdCard { get; set; }
    }
}
