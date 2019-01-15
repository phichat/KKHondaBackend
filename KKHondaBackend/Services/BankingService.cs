using System;
using System.Collections.Generic;
using System.Linq;
using KKHondaBackend.Data;
using KKHondaBackend.Models;

namespace KKHondaBackend.Services
{
    public class BankingService: IBankingService
    {
        private readonly dbwebContext ctx;

        public BankingService(dbwebContext context)
        {
            ctx = context;
        }

        public Banking[] GetBanking()
        {
            return ctx.Bankings.Where(x => x.Status == true).ToArray();
        }

        public Dropdown[] GetDropdowns()
        {
            var list = new List<Dropdown>();
            list = ctx.Bankings.Where(x => x.Status == true)
                .Select(x => new Dropdown
                {
                    Value = x.BankCode,
                    Text = x.BankName
                }).ToList();

            return list.ToArray();
        }
    }
}
