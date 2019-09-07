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

        public string GeerateeReturnDepositNo(int branchId)
        {
            var depNo = (from db in ctx.Booking
                         orderby db.ReturnDepNo descending
                         where db.BranchId == branchId
                         select db.ReturnDepNo).FirstOrDefault();

            return SetRunningCode("DEPR", branchId, depNo);
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

        public string GenerateInstalmentTaxInvoiceNo(int branchId)
        {
            var invNo = (from db in ctx.CreditContractPayment
                         orderby db.TaxInvoiceNo descending
                         where db.TaxInvoiceBranchId == branchId
                         select db.TaxInvoiceNo).FirstOrDefault();

            return SetRunningCode("TF", branchId, invNo);
        }

        public string GenerateReceiptNo(int branchId)
        {
            var receiptNo = (from db in ctx.CreditContractPayment
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

        public string GenerateHistoryCarNo(int branchId) {
            var no = (from db in ctx.CarHistory
                      orderby db.CarId descending
                      where db.BranchId == branchId
                      select db.CarNo).FirstOrDefault();

            return SetRunningCode("PRB", branchId, no);
        }

        public string GenerateConNo(int branchId){
            var no = (from db in ctx.CarRegisList
                      orderby db.BookingId descending
                      where db.BranchId == branchId
                      select db.BookingNo).FirstOrDefault();

            return SetRunningCode("CON", branchId, no);
        }

        public string GenerateSedNo(int branchId)
        {
            var no = (from db in ctx.CarRegisSedList
                      orderby db.SedId descending
                      where db.BranchId == branchId
                      select db.SedNo).FirstOrDefault();

            return SetRunningCode("SED", branchId, no);
        }

        public string GenerateAlNo(int branchId)
        {
            var no = (from db in ctx.CarRegisAlList
                      orderby db.AlId descending
                      where db.BranchId == branchId
                      select db.AlNo).FirstOrDefault();

            return SetRunningCode("AL", branchId, no);
        }

        public string GenerateClNo(int branchId)
        {
            var no = (from db in ctx.CarRegisClList
                      orderby db.ClId descending
                      where db.BranchId == branchId
                      select db.ClNo).FirstOrDefault();

            return SetRunningCode("CL", branchId, no);
        }

        public string GenerateRegisRevNo(int branchId)
        {
            var no = (from db in ctx.CarRegisRevList
                      orderby db.RevId descending
                      where db.BranchId == branchId
                      select db.RevNo).FirstOrDefault();

            return SetRunningCode("REV", branchId, no);
        }

        public string GetSysParameter(string prefix)
        {
            throw new NotImplementedException();
        }

        private string SetRunningCode(string prefix, int branchId, string runningNumber)
        {
            string year = (DateTime.Now.Year + 543).ToString().Substring(2, 2);
            string month = (DateTime.Now.Month).ToString("00");
            string r = $"{prefix}{branchId.ToString("00")}{year}{month}";

            if (runningNumber == null) return $"{r}/0001";

            string preStr = runningNumber.Split("/")[0];
            string endStr = runningNumber.Split("/")[1];

            string preMonth = preStr.Substring(preStr.Length - 2);
            int runNumber = (preMonth == month) ? int.Parse(endStr) + 1 : 1;
            return $"{r}/{runNumber.ToString("0000")}";
        }
    }
}
