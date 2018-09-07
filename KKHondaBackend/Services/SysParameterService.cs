using System;
using System.Linq;
using System.Collections.Generic;
using KKHondaBackend.Data;

namespace KKHondaBackend.Services
{
    public class SysParameterService : ISysParameterService
    {
        private readonly dbwebContext ctx;

        public SysParameterService(dbwebContext context)
        {
            ctx = context;
        }

        public string GenerateContractNo(int branchId)
        {
            var contractNo = (from db in ctx.CreditContract
                              orderby db.ContractNo descending
                              where db.BranchId == branchId
                              select db.ContractNo
                             ).FirstOrDefault();
            
            return SetRunningCode("CO", branchId, contractNo);
        }

        public string GetnerateInstalmentTaxInvoiceNo(int branchId)
        {
            var invNo = (from db in ctx.CreditContractItem
                        orderby db.TaxInvoiceNo descending
                        where db.TaxInvoiceBranchId == branchId
                        select db.TaxInvoiceNo).FirstOrDefault();

            return SetRunningCode("TF", branchId, invNo);
        }

        public string GetnerateReceiptNo(int branchId) {
            var receiptNo = (from db in ctx.CreditContractItem
                      orderby db.ReceiptNo descending
                      where db.TaxInvoiceBranchId == branchId
                      select db.ReceiptNo).FirstOrDefault();

            return SetRunningCode("OP", branchId, receiptNo);
        }

        public string GenerateSellNo(int branchId)
        {
            var sellNo = (from db in ctx.Booking
                          orderby db.SellNo descending
                          where db.BranchId == branchId
                          select db.SellNo
                         ).FirstOrDefault();

            return SetRunningCode("SR", branchId, sellNo);
        }

        public string GenerateVatNo(int branchId)
        {
            var vatNo = (from db in ctx.Booking
                         orderby db.VatNo descending
                         where db.BranchId == branchId
                         select db.VatNo
                       ).FirstOrDefault();

            return SetRunningCode("MC", branchId, vatNo);
        }
                
        public string GetSysParameter(string prefix)
        {
            throw new NotImplementedException();
        }

        private string SetRunningCode(string prefix, int branchId, string runningNumber)
        {
            string year = (DateTime.Now.Year + 543).ToString().Substring(2, 2);
            string month = (DateTime.Now.Month).ToString("00");

            if (runningNumber == null)
                return prefix + branchId.ToString("00") + year + month + "/" + "0001";

            string preMonth = runningNumber.Substring(6, 2);
            int runNumber = (preMonth == month) ? int.Parse(runningNumber.Split("/")[1]) + 1 : 1;
            return prefix + branchId.ToString("00") + year + month + "/" + runNumber.ToString("0000");
        }
    }
}
