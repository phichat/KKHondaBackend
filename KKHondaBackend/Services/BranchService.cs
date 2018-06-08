using System;
using System.Linq;
using System.Collections.Generic;
using KKHondaBackend.Data;

namespace KKHondaBackend.Services
{
    public class BranchService : IBranchService
    {
        private readonly dbwebContext ctx;
        public BranchService(dbwebContext context)
        {
            ctx = context;
        }

        public Dropdown[] GetDropdowns()
        {
            List<Dropdown> dropdowns = new List<Dropdown>();

            dropdowns = ctx.Branch
                           .Where(o => o.BranchEnable == 1)
                           .Select(o => new Dropdown
                           {
                               Value = o.BranchId.ToString(),
                               Text = o.BranchName
                           }).ToList();
            return dropdowns.ToArray();
        }
    }
}
