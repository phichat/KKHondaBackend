using System;
using System.Linq;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KKHondaBackend.Services
{
  public interface ISaleInvTaxRecService
  {
    SaleInvTaxRec SetSaleInvTaxRec(string branchTax, string branch, MCustomer cHire);
  }

  public class SaleInvTaxRecService : ISaleInvTaxRecService
  {
    private readonly dbwebContext _context;
    public SaleInvTaxRecService(
      dbwebContext context
    )
    {
      _context = context;
    }
    public SaleInvTaxRec SetSaleInvTaxRec(string branchTax, string branch, MCustomer cHire)
    {
      var cAddress = cHire.MCustomerAddress.FirstOrDefault();
      var amphor = _context.MAmphor.Where(x => x.AmphorCode == cAddress.AmphorCode).AsNoTracking().FirstOrDefault();
      var province = _context.MProvince.Where(x => x.ProvinceCode == cAddress.ProvinceCode).AsNoTracking().FirstOrDefault();
      var ingTaxRec = new SaleInvTaxRec
      {
        // InvTaxRecNo = iSysParamService.GenerateVatNo(branchId),
        InvTaxRecDate = DateTime.Now,
        Status = true,
        CustomerCode = cHire.CustomerCode,
        CustomerFullName = $"{cHire.CustomerPrename}{cHire.CustomerName} {cHire.CustomerSurname}",
        CustomerFullAddress = $"{cAddress.Address} อำเภอ{amphor.AmphorName} จังหวัด{province.ProvinceNameTh} {amphor.Zipcode}",
        BranchTax = branchTax,
        Branch = branch,
      };
      return ingTaxRec;
    }

  }
}