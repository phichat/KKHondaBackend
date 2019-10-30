﻿using System;
using System.Collections.Generic;
using System.Linq;
using KKHondaBackend.Data;
using KKHondaBackend.Models;

namespace KKHondaBackend.Services
{
  public interface IBankingService
  {
    Banking[] GetBanking();
    IEnumerable<BankingDetail> GetBankingAndDetail();
    Dropdown[] GetDropdowns();
  }
  public class BankingService : IBankingService
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

    public IEnumerable<BankingDetail> GetBankingAndDetail()
    {
      var list = (from h in ctx.Bankings
                  join d in ctx.BankingAcc on h.BankCode equals d.AccBankCode into r
                  from result in r.DefaultIfEmpty()
                  where h.Status == true
                  select new BankingDetail
                  {
                    BankCode = h.BankCode,
                    BankName = h.BankName,
                    AccBankNumber = result.AccBankNumber,
                    AccBankName = result.AccBankName,
                    AccBankType = result.AccBankType,
                    AccountType = result.AccountType,
                  }).ToList();
      return list;
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
