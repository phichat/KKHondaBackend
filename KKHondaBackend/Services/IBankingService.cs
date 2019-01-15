using System;
using KKHondaBackend.Models;

namespace KKHondaBackend.Services
{
    public interface IBankingService
    {
        Banking[] GetBanking();
        Dropdown[] GetDropdowns();
    }
}
