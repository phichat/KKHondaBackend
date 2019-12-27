using System;
using System.Linq;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KKHondaBackend.Services
{
  public interface ISaleCommissionService
  {
    SaleCommission SetSaleCommission(SaleFormBody reserve, MCustomer cHire, int branchId);
  }

  public class SaleCommissionService : ISaleCommissionService
  {
    private readonly dbwebContext _context;
    public SaleCommissionService(
      dbwebContext context
    )
    {
      _context = context;
    }

    public SaleCommission SetSaleCommission(SaleFormBody reserve, MCustomer cHire, int branchId)
    {
      var cAddress = cHire.MCustomerAddress.FirstOrDefault();
      var amphor = _context.MAmphor.Where(x => x.AmphorCode == cAddress.AmphorCode).AsNoTracking().FirstOrDefault();
      var province = _context.MProvince.Where(x => x.ProvinceCode == cAddress.ProvinceCode).AsNoTracking().FirstOrDefault();
      var branch = _context.Branch.Where(x => x.BranchId == branchId).AsNoTracking().FirstOrDefault();
      var saleCom = new SaleCommission
      {
        // ComNo = iSysParamService.GenerateTaxInvNo(branchId),
        ComDate = DateTime.Now,
        ComPrice = (decimal)reserve.ComPrice,
        Status = true,
        CustomerCode = cHire.CustomerCode,
        CustomerFullName = $"{cHire.CustomerPrename}{cHire.CustomerName} {cHire.CustomerSurname}",
        CustomerFullAddress = $"{cAddress.Address} อำเภอ{amphor.AmphorName} จังหวัด{province.ProvinceNameTh} {amphor.Zipcode}",
        BranchTax = branch.BranchRegisterNo,
        Branch = branch.BranchName,
        FiId = (int)reserve.FiId,
        FiintId = (int)reserve.FiintId,
        FiComId = (int)reserve.FiComId,
      };
      return saleCom;
    }

  }
}