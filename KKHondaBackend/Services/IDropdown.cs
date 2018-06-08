using System;
namespace KKHondaBackend.Services
{
    public interface IDropdown
    {
        Dropdown[] Dropdowns();
    }

    public class Dropdown {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
