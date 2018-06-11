using System;
namespace KKHondaBackend.Services
{
    public interface IZoneService
    {
        Dropdown[] GetDropdowns();

        Dropdown GetDropdownById(int id);
    }
}
