using System;
namespace KKHondaBackend.Services
{
    public interface IBranchService
    {
        Dropdown[] GetDropdowns();

        Dropdown GetDropdownById(int id);
    }
}
