using System;
using System.Linq;
using System.Collections.Generic;
using KKHondaBackend.Data;
using KKHondaBackend.Models;

namespace KKHondaBackend.Services
{
  public interface IStatusService
  {
    Dropdown[] GetDropdown();
    Dropdown[] HPSDropdown();
    Dropdown[] GetDropdownTypePayment();
  }
  public class StatusService : IStatusService
  {

    private readonly dbwebContext ctx;

    public StatusService(dbwebContext context)
    {
      ctx = context;
    }

    public Dropdown[] GetDropdown()
    {
      List<Dropdown> dropdown = new List<Dropdown>();
      dropdown = (from db in ctx.MStatus
                  where db.Status == true
                  select new Dropdown
                  {
                    Value = db.Id.ToString(),
                    Text = db.StatusDesc
                  }).ToList();

      return dropdown.ToArray();
    }

    public Dropdown[] HPSDropdown()
    {
      List<Dropdown> dropdown = new List<Dropdown>();
      dropdown = (from db in ctx.MStatus
                  where db.Status == true &&
                  (db.Id >= 16 && db.Id <= 33)
                  select new Dropdown
                  {
                    Value = db.Id.ToString(),
                    Text = db.StatusDesc
                  }).ToList();
      return dropdown.ToArray();
    }

    public Dropdown[] GetDropdownTypePayment()
    {
      var dropdown = new List<Dropdown>();
      dropdown = (from db in ctx.MStatus
                  where db.Status == true &&
                  (db.Id == 10 ||
                   db.Id == 11 ||
                   db.Id == 12 ||
                   db.Id == 13
                  )
                  select new Dropdown
                  {
                    Value = db.Id.ToString(),
                    Text = db.StatusDesc
                  }).ToList();
      return dropdown.ToArray();
    }
  }
}
