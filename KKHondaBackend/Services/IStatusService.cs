﻿using System;
namespace KKHondaBackend.Services
{
    public interface IStatusService
    {
        Dropdown[] GetDropdown();
        Dropdown[] GetDropdownCredit();
        Dropdown[] GetDropdownTypePayment();
    }
}
