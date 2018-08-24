using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;

namespace KKHondaBackend.Controllers.Credits
{
    [Produces("application/json")]
    [Route("api/Credit/Contract/CreditPayment")]
    public class CreditPaymentController : Controller
    {
        private readonly dbwebContext ctx;

        public CreditPaymentController(dbwebContext _ctx)
        {
            ctx = _ctx;
        }

        // GET: api/CreditPayment/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            try
            {
                var contract = (from db in ctx.CreditContract

                                join _branch in ctx.Branch on db.BranchId equals _branch.BranchId into a1
                                from branch in a1.DefaultIfEmpty()

                                join _status in ctx.MStatus on db.ContractStatus equals _status.Id into a5
                                from status in a5.DefaultIfEmpty()

                                join _contrachHire in ctx.MCustomer on db.ContractHire equals _contrachHire.CustomerCode into a7
                                from contrachHire in a7.DefaultIfEmpty()

                                join _sale in ctx.User on db.CreatedBy equals _sale.Id into a14
                                from sale in a14.DefaultIfEmpty()

                                where db.ContractId == id

                                select new
                                {
                                    ContractId = id,
                                    CalculateId = db.CalculateId,
                                    BookingId = db.BookingId,
                                    RefNo = db.RefNo,
                                    ContractNo = db.ContractNo,
                                    ContractDate = db.ContractDate,
                                    StatusDesc = status.StatusDesc,
                                    ContractHire = contrachHire.CustomerPrename + contrachHire.CustomerName + " " + contrachHire.CustomerSurname,
                                    SaleName = sale.Fullname,
                                    Remark = db.Remark
                                }).SingleOrDefault();

                var calculate = ctx.CreditCalculate.SingleOrDefault(p => p.CalculateId == contract.CalculateId);

                var booking = (from item in ctx.BookingItem

                               join bra in ctx.ProductBrand on item.BrandId equals bra.BrandId into a1
                               from brand in a1.DefaultIfEmpty()

                               join col in ctx.ProductColor on item.ColorId equals col.ColorId into a4
                               from color in a4.DefaultIfEmpty()

                               join mod in ctx.ProductModel on item.ModelId equals mod.ModelId into a5
                               from model in a5.DefaultIfEmpty()

                               join _transferlog in ctx.TransferLog on item.ItemId equals _transferlog.ItemId into a6
                               from transferlog in a6.DefaultIfEmpty()

                               where item.BookingId == id && item.ItemDetailType == 1
                               select new
                               {
                                   EngineNo = transferlog.EngineNo,
                                   FrameNo = transferlog.FrameNo,
                                   BrandName = brand.BrandName,
                                   ModelCode = model.ModelCode,
                                   Color = color.ColorName,
                                   Price = calculate.Remain
                               }).SingleOrDefault();

                var isPay = ctx.CreditContractItem
                    .Where(x => x.ContractId == id && x.RefNo == contract.RefNo && x.PayNetPrice != null)
                    .GroupBy(x => x.RefNo, (key, values) => new
                    {
                        IsPayPrice = values.Sum(x => x.PayNetPrice),
                        IsPayTerm = values.Count()
                    }).SingleOrDefault();

                var isOutstanding = ctx.CreditContractItem
                    .Where(x => x.ContractId == id && x.RefNo == contract.RefNo && x.PayNetPrice == null)
                    .GroupBy(x => x.RefNo, (key, values) => new
                    {
                        IsOutstandingPrice = values.Sum(x => x.BalanceNetPrice),
                        IsOutstandingTerm = values.Count()
                    }).SingleOrDefault();

                var contractItem = (from db in ctx.CreditContractItem
                                    where db.ContractId == id && db.RefNo == contract.RefNo

                                    join _user in ctx.User on db.Payeer equals _user.Id into a1
                                    from user in a1.DefaultIfEmpty()

                                    select new
                                    {
                                        ContractItemId = db.ContractItemId,
                                        InstalmentNo = db.InstalmentNo,
                                        TaxInvoiceNo = db.TaxInvoiceNo,
                                        DueDate = db.DueDate,
                                        PayDate = db.PayDate,
                                        BalanceNetPrice = db.BalanceNetPrice,
                                        PayNetPrice = db.PayNetPrice,
                                        PaymentType = db.PaymentType,
                                        Remark = db.Remark,
                                        Payeer = user.Fullname
                                    }).ToList();

                var obj = new Dictionary<string, object>
                {
                    {"contract", contract},
                    {"booking", booking},
                    {"contractItem", contractItem},
                    {"isPay", isPay},
                    {"isOutstanding", isOutstanding}
                };

                return Ok(obj);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // POST: api/CreditPayment
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CreditPayment/5
        [HttpPost("PaymentTerm")]
        public IActionResult PaymentTerm([FromBody] Payment payment)
        {
            try
            {
                return Get(1);
            } catch(Exception)
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult CancelContractTerm(int id)
        {
            try
            {
                var creditContractItem = ctx.CreditContractItem.SingleOrDefault(item => item.ContractItemId == id);
                creditContractItem.PayDate = null;
                creditContractItem.PaymentType = null;
                creditContractItem.PayPrice = null;
                creditContractItem.PayVatPrice = null;
                creditContractItem.PayNetPrice = null;
                ctx.SaveChanges();

                return Get(creditContractItem.ContractId);

            } catch(Exception)
            {
                return StatusCode(500);
            }
        }

        //public class PaymentTerm
        //{
        //    public List<Payment> payment { get; set; }
        //}

        public partial class Payment
        {
            public int ContractItemId { get; set; }
            public int ContractId { get; set; }
            public string Payeer { get; set; }
            public DateTime PayDate { get; set; }
            public int PaymentType { get; set; }
        }
    }
}
