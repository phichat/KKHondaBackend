using System;
namespace KKHondaBackend.Services
{
    public interface ICustomerServices
    {
        CustomerDropdown[] GetCustomerDropdownByKey(string term);

        CustomerDropdown[] GetCustomerTop100Dropdowns();
    }

    public class CustomerDropdown {
        public string CustomerCode { get; set; }
        public string CustomerFullName { get; set; }
    }
}
