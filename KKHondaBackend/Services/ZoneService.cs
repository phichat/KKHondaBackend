using System;
using System.Collections.Generic;
using System.Linq;
using KKHondaBackend.Data;

namespace KKHondaBackend.Services
{
    public class ZoneService : IZoneService
    {
        private readonly dbwebContext ctx;

        public ZoneService(dbwebContext context)
        {
            ctx = context;
        }

        public Dropdown[] GetDropdowns()
        {
            List<Dropdown> zoneDropdowns = new List<Dropdown>();

            zoneDropdowns = ctx.Zone
                               .Select(o => new Dropdown
                               {
                                   Value = o.ZoneId.ToString(),
                                   Text = o.ZoneName
                               }).ToList();
            return zoneDropdowns.ToArray();
        }

        public Dropdown GetDropdownById(int id)
        {
            Dropdown dropdown = new Dropdown();
            dropdown = ctx.Zone
                .Where(z => z.ZoneId == id)
                .Select(z => new Dropdown
                {
                    Value = z.ZoneId.ToString(),
                    Text = z.ZoneName
                }).SingleOrDefault();
            return dropdown;
        }

    }
}
