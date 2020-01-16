using System;
using System.Linq;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KKHondaBackend.Services
{
  public interface ISaleCommissionService
  {
    SaleCommission SetSaleCommission(decimal comPrice, MCustomer cHire);
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

    public SaleCommission SetSaleCommission(decimal comPrice, MCustomer cHire)
    {
      // ข้อมูลในใบส่งเสริมการขายชื่อลูกค้าเป็น บริษัทไฟแนนซ์
      var cAddress = cHire.MCustomerAddress.FirstOrDefault();
      var amphor = _context.MAmphor.Where(x => x.AmphorCode == cAddress.AmphorCode).AsNoTracking().FirstOrDefault();
      var province = _context.MProvince.Where(x => x.ProvinceCode == cAddress.ProvinceCode).AsNoTracking().FirstOrDefault();
      var saleCom = new SaleCommission
      {
        ComDate = DateTime.Now,
        ComPrice = comPrice,
        Status = true,
        CustomerCode = cHire.CustomerCode,
        CustomerFullName = $"{cHire.CustomerPrename}{cHire.CustomerName} {cHire.CustomerSurname}",
        CustomerFullAddress = $"{cAddress.Address} อำเภอ{amphor.AmphorName} จังหวัด{province.ProvinceNameTh} {amphor.Zipcode}",
        BranchTax = cHire.IdCard,
        Branch = $"{cHire.CustomerPrename}{cHire.CustomerName} {cHire.CustomerSurname}"
      };
      return saleCom;
    }

  }
}