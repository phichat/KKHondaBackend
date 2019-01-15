using System;
using System.Linq;
using System.Collections.Generic;
using KKHondaBackend.Data;
using KKHondaBackend.Models;

namespace KKHondaBackend.Services
{
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

        public Dropdown[] GetDropdownCredit(){
            List<Dropdown> dropdown = new List<Dropdown>();
            dropdown = (from db in ctx.MStatus
                        where db.Status == true && 
                        (db.Id == 16 ||
                         db.Id == 17 ||
                         db.Id == 18 ||
                         db.Id == 19 ||
                         db.Id == 20 ||
                         db.Id == 21 ||
                         db.Id == 23 ||
                         db.Id == 27 ||
                         db.Id == 28 ||
                         db.Id == 29 ||
                         db.Id == 30 ||
                         db.Id == 31 ||
                         db.Id == 32
                        )
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
