using System;
using System.Linq;
using System.Collections.Generic;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using KKHondaBackend.Entities;

namespace KKHondaBackend.Services
{
  public interface ISysParameterService
  {
    string GetSysParameter(string prefix);
    string GeerateeDepositNo(int branchId);
    string GeerateeReturnDepositNo(int branchId);
    string GenerateSellNo(int branchId);
    string GenerateVatNo(int branchId);
    string GenerateTaxInvNo(int branchId);
    string GenerateReceiptNo(int branchId);
    string GenerateContractNo(int branchId);
    string GenerateConNo(int branchId);
    string GenerateHistoryCarNo(int branchId);
    string GenerateSedNo(int branchId);
    string GenerateAlNo(int branchId);
    string GenerateClNo(int branchId);
    string GenerateRegisRevNo(int branchId);
    string GenerateRegisCLDepositNo(int branchId);
    string GenerateReceiveNo(int branchId);
  }

  public class SysParameterService : ISysParameterService
  {
    private readonly dbwebContext ctx;

    public SysParameterService(dbwebContext context)
    {
      ctx = context;
    }
    public string GeerateeDepositNo(int branchId)
    {
      return SetRunningCode("RESERVE", "PR", branchId);
    }
    public string GeerateeReturnDepositNo(int branchId)
    {
      return SetRunningCode("RESERVE", "DEPR", branchId);
    }
    public string GenerateSellNo(int branchId)
    {
      return SetRunningCode("SALE", "SR", branchId);
    }
    public string GenerateVatNo(int branchId)
    {
      return SetRunningCode("SALE", "MC", branchId);
    }
    public string GenerateTaxInvNo(int branchId)
    {
      return SetRunningCode("SALE", "TF", branchId);
    }
    public string GenerateReceiptNo(int branchId)
    {
      return SetRunningCode("SALE", "RC", branchId);
    }
    public string GenerateContractNo(int branchId)
    {
      return SetRunningCode("CONTRACT", "CO", branchId);
    }
    public string GenerateHistoryCarNo(int branchId)
    {
      return SetRunningCode("RIS", "PRB", branchId);
    }

        public string GenerateConNo(int branchId)
        {
            return SetRunningCode("RIS", "CON", branchId);
        }

        public string GenerateSedNo(int branchId)
        {
            return SetRunningCode("RIS", "SED", branchId);
        }

        public string GenerateAlNo(int branchId)
        {
            return SetRunningCode("RIS", "AL", branchId);
        }

        public string GenerateClNo(int branchId)
        {
            return SetRunningCode("RIS", "CL", branchId);
        }

        public string GenerateRegisRevNo(int branchId)
        {
            return SetRunningCode("RIS", "REV", branchId);
        }

        public string GenerateRegisCLDepositNo(int branchId)
        {
            return SetRunningCode("RIS", "PFD", branchId);
        }


    public string GenerateReceiveNo(int branchId)
    {
      return SetRunningCode("STOCK", "GR", branchId);
    }

    public string GetSysParameter(string prefix)
    {
      throw new NotImplementedException();
    }

    private string SetRunningCode(string module, string prefix, int branchId)
    {
      var paramHd = ctx.MParameter
          .Where(x => x.Prefix == prefix)
          .FirstOrDefault();

      if (paramHd == null)
      {
        paramHd.Module = module;
        paramHd.Prefix = prefix;
        ctx.Add(paramHd);
        ctx.SaveChanges();
      }

      string year = (DateTime.Now.Year + 543).ToString().Substring(2, 2);
      string month = (DateTime.Now.Month).ToString("00");
      string runningNo = string.Empty;

      var branchCode = ctx.Branch
        .Where(x => x.BranchId == branchId)
        .Select(x => int.Parse(x.BranchCode).ToString("00"))
        .FirstOrDefault();

      var paramDt = ctx.MParameterD
        .Where(dt =>
            dt.ParamHdId == paramHd.ParamHdId &&
            dt.Branch == branchCode)
        .FirstOrDefault();

      var r = $"{prefix}{branchCode}{year}{month}";
      if (paramDt == null)
      {
        var p = new MParameterD
        {
          ParamHdId = paramHd.ParamHdId,
          Branch = branchCode,
          Year = year,
          Month = month,
          RunningNo = 1,
        };
        ctx.Add(p);
        ctx.SaveChanges();
        runningNo = (1).ToString("0000");
      }
      else
      {
        if (paramDt.Month == month && paramDt.Year == year)
        {
          paramDt.RunningNo += 1;
        }
        else
        {
          paramDt.Year = year;
          paramDt.Month = month;
          paramDt.RunningNo = 1;
        }
        ctx.Update(paramDt);
        ctx.SaveChanges();
        runningNo = paramDt.RunningNo.ToString("0000");
      }
      return $"{r}/{runningNo}";

    }
}
