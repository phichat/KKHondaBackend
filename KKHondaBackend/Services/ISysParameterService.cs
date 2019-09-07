using System;
namespace KKHondaBackend.Services
{
    public interface ISysParameterService
    {
        string GetSysParameter(string prefix);
        string GenerateSellNo(int branchId);
        string GenerateVatNo(int branchId);
        string GenerateContractNo(int branchId);
        string GenerateInstalmentTaxInvoiceNo(int branchId);
        string GenerateReceiptNo(int branchId);
        string GeerateeReturnDepositNo(int branchId);
        string GenerateConNo(int branchId);
        string GenerateHistoryCarNo(int branchId);
        string GenerateSedNo(int branchId);
        string GenerateAlNo(int branchId);
        string GenerateClNo(int branchId);
        string GenerateRegisRevNo(int branchId);
    }
}
