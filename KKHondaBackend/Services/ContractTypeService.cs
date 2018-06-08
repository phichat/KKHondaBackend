using System;
using System.Linq;
using System.Collections.Generic;
using KKHondaBackend.Data;

namespace KKHondaBackend.Services
{
    public class ContractTypeService : IContractTypeService
    {
        private readonly dbwebContext ctx;

        public ContractTypeService(dbwebContext context)
        {
            ctx = context;
        }

        public Dropdown[] GetDropdowns()
        {
            List<Dropdown> contractTypeDropdowns = new List<Dropdown>();

            contractTypeDropdowns = ctx.MContractTypes
                                       .Where(o => o.Status == true)
                                       .Select(o => new Dropdown
                                       {
                                           Value = o.Id.ToString(),
                                           Text = o.TypeDesc
                                       }).ToList();

            return contractTypeDropdowns.ToArray();
        }
    }
}
