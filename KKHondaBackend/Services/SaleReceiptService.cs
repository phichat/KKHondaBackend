using System;
using System.Linq;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KKHondaBackend.Services
{
  public interface ISaleReceiptService
  {
    SaleReceipt SetSaleReceipt(MCustomer cHire, int branchId);
  }

  public class SaleReceiptService : ISaleReceiptService
  {
    private readonly dbwebContext _context;
    public SaleReceiptService(
      dbwebContext context
    )
    {
      _context = context;
    }

    public SaleReceipt SetSaleReceipt(MCustomer cHire, int branchId)
    {
      var cAddress = cHire.MCustomerAddress.FirstOrDefault();
      var amphor = _context.MAmphor.Where(x => x.AmphorCode == cAddress.AmphorCode).AsNoTracking().FirstOrDefault();
      var province = _context.MProvince.Where(x => x.ProvinceCode == cAddress.ProvinceCode).AsNoTracking().FirstOrDefault();
      var branch = _context.Branch.Where(x => x.BranchId == branchId).AsNoTracking().FirstOrDefault();
      var receipt = new SaleReceipt
      {
        // ReceiptNo = iSysParamService.GenerateReceiptNo(branchId),
        ReceiptDate = DateTime.Now,
        Status = true,
        CustomerCode = cHire.CustomerCode,
        CustomerFullName = $"{cHire.CustomerPrename}{cHire.CustomerName} {cHire.CustomerSurname}",
        CustomerFullAddress = $"{cAddress.Address} อำเภอ{amphor.AmphorName} จังหวัด{province.ProvinceNameTh} {amphor.Zipcode}",
        BranchTax = branch.BranchRegisterNo,
        Branch = branch.BranchName,
      };
      return receipt;
    }

  }
}