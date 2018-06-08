using System;
namespace KKHondaBackend.Services
{
    public interface ICustomerServices
    {
        Dropdown[] GetDropdownByKey(string term);

        Dropdown[] GetDropdowns();
    }
}
