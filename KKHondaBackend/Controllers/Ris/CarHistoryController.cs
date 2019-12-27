using System;
using KKHondaBackend.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KKHondaBackend.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace KKHondaBackend.Controllers.Ris
{
  // [ApiController]
  [Produces("application/json")]
  [Route("api/Ris/[controller]")]
  public class CarHistoryController : Controller
  {
    private readonly dbwebContext ctx;
    public CarHistoryController(dbwebContext _ctx)
    {
      ctx = _ctx;
    }

    private IEnumerable<CarHistory> HistoryList
    {
      get => (from h in ctx.CarHistory
                // join ow in ctx.MCustomer on h.OwnerCode equals ow.CustomerCode into _ow
                // join vi in ctx.MCustomer on h.VisitorCode equals vi.CustomerCode into _vi
                // from own in _ow.DefaultIfEmpty()
                // from vis in _vi.DefaultIfEmpty()
              select new CarHistory
              {
                CarId = h.CarId,
                CarNo = h.CarNo,
                BookingId = h.BookingId,
                ENo = h.ENo,
                FNo = h.FNo,
                TagNo = h.TagNo,
                Province = h.Province,
                BranchId = h.BranchId,
                TagRegis = h.TagRegis,
                TagExpire = h.TagExpire,
                PrbNo = h.PrbNo,
                PrbCompany = h.PrbCompany,
                PrbRegis = h.PrbRegis,
                PrbExpire = h.PrbExpire,
                CommitNo = h.CommitNo,
                CommitExpire = h.CommitExpire,
                WarNo = h.WarNo,
                WarCompany = h.WarCompany,
                WarRegis = h.WarRegis,
                WarExpire = h.WarExpire,
                OwnerCode = h.OwnerCode,
                // OwnerName = own != null ? $"{own.CustomerPrename}{own.CustomerName} {own.CustomerSurname}" : null,
                VisitorCode = h.VisitorCode,
                // VisitorName = vis != null ? $"{vis.CustomerPrename}{vis.CustomerName} {vis.CustomerSurname}" : null
              }).AsNoTracking();
    }

    [HttpGet("GetByBookingId")]
    public IActionResult GetByBookingId(int bookingId)
    {
      var list = ctx.CarHistory.FirstOrDefault(x => x.BookingId == bookingId);
      return Ok(list);
    }

    [HttpGet("SearchByEngine")]
    public IActionResult SearchByEngine(string term)
    {
      var p1 = new SqlParameter("@term", term);
      var query = $@"SELECT h.*
            FROM dbweb.dbo._car_history as h
                INNER JOIN (SELECT MAX(car_id) car_id, e_no, f_no
                FROM dbweb.dbo._car_history
                GROUP BY e_no, f_no) as g on h.car_id = g.car_id
            WHERE g.e_no LIKE '%'+@term+'%' OR g.f_no LIKE '%'+@term+'%'";

      var list = ctx.CarHistory.FromSql(query).AsNoTracking().ToList();
      return Ok(list);
    }
  }
}