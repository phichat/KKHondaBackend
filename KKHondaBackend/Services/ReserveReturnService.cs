using System;
using System.Linq;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KKHondaBackend.Services
{
  public interface IReserveReturnService
  {
    ReserveReturn SetReserve(SaleFormBody reserve, MCustomer cBooking);
  }

  public class ReserveReturnService : IReserveReturnService
  {
    private readonly dbwebContext _context;
    public ReserveReturnService(
      dbwebContext context
    )
    {
      _context = context;
    }

    public ReserveReturn SetReserve(SaleFormBody reserve, MCustomer cBooking)
    {
      var cAddress = cBooking.MCustomerAddress.FirstOrDefault();
      var amphor = _context.MAmphor.Where(x => x.AmphorCode == cAddress.AmphorCode).AsNoTracking().FirstOrDefault();
      var province = _context.MProvince.Where(x => x.ProvinceCode == cAddress.ProvinceCode).AsNoTracking().FirstOrDefault();
      var deposit = new ReserveReturn
      {
        BookingId = reserve.BookingId,
        // ReturnDepositNo = iSysParamService.GeerateeReturnDepositNo(branchId),
        ReturnDepositDate = DateTime.Now,
        Status = true,
        CustomerCode = cBooking.CustomerCode,
        CustomerFullName = $"{cBooking.CustomerPrename}{cBooking.CustomerName} {cBooking.CustomerSurname}",
        CustomerFullAddress = $"{cAddress.Address} อำเภอ{amphor.AmphorName} จังหวัด{province.ProvinceNameTh} {amphor.Zipcode}",
        PaymentType = 1,
        PaymentPrice = reserve.ReturnDepositPrice,
        DiscountPrice = 0,
        TotalPaymentPrice = reserve.ReturnDepositPrice,
        PaymentDate = DateTime.Now
      };
      return deposit;
    }
  }
}