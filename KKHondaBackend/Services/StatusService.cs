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
    }
}
