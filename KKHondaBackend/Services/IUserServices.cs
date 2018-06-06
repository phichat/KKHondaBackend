using System;
namespace KKHondaBackend.Services
{
    public interface IUserServices
    {
        UserDropdown[] GetAllUserDropdowns();
    }

    public class UserDropdown
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
