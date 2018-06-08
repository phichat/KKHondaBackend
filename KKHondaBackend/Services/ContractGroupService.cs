using System;
using System.Linq;
using System.Collections.Generic;
using KKHondaBackend.Data;

namespace KKHondaBackend.Services
{
    public class ContractGroupService : IContractGroupService
    {
        private readonly dbwebContext ctx;

        public ContractGroupService(dbwebContext context)
        {
            ctx = context;
        }

        public Dropdown[] GetDropdowns()
        {
            List<Dropdown> contractGroupDropdowns = new List<Dropdown>();

            contractGroupDropdowns = ctx.MContractGroups
                                        .Where(o => o.Status == true)
                                        .Select(o => new Dropdown
                                        {
                                            Value = o.Id.ToString(),
                                            Text = o.GroupDesc
                                        }).ToList();

            return contractGroupDropdowns.ToArray();
        }
    }
}
