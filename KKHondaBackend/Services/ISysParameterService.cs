using System;
namespace KKHondaBackend.Services
{
    public interface ISysParameterService
    {
        string GetSysParameter(string prefix);

        string GenerateSellNo(int branchId);

        string GenerateVatNo(int branchId);

        string GenerateContractNo(int branchId);

    }
}
