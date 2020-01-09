using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using KKHondaBackend.Services;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using KKHondaBackend.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KKHondaBackend.Controllers.Credits
{
    //   [ApiController]
    [Produces("application/json")]
    [Route("api/Credit/Contract")]
    public class CreditContractController : Controller
    {

    

        private IEnumerable<CreditContractItem> ListContractItems(int contractId, string refNo)
        {
            return ctx.CreditContractItem
                    .Where(p => p.ContractId == contractId && p.RefNo == refNo)
                    .OrderBy(o => o.InstalmentNo).ToList();
        }

    [HttpGet("GetContractItem")]
    public IActionResult GetContractItem(int contractId)
    {
      var contract = ctx.CreditContract.AsNoTracking().First(x => x.ContractId == contractId);
      return Ok(ListContractItems(contractId, contract.RefNo));
    }

        // GET api/values/5
        [HttpGet("GetById")]
        public IActionResult Get(int id)
        {
            try
            {
                var cont = ctx.CreditContract.Where(p => p.ContractId == id).SingleOrDefault();

        var calcu = ctx.Sale.Where(p => p.SaleId == cont.SaleId).SingleOrDefault();

        var booking = iBookService.GetBookingById(cont.BookingId);
        if (booking.BookingPaymentType == BookingPaymentType.HierPurchase)
        {
          var __branch = ctx.Branch.SingleOrDefault(x => x.BranchId == cont.BranchId);

          var __company = ctx.MCustomer.Where(x => x.CustomerCode == cont.ContractOwner).FirstOrDefault();
          var ampher = ctx.MAmphor.Where(x => x.AmphorCode == cont.OwnerAmpherCode).FirstOrDefault();
          var province = ctx.MProvince.Where(x => x.ProvinceCode == cont.OwnerProvinceCode).FirstOrDefault();
          booking.CusTaxNo = cont.OwnerTaxNo;
          booking.CusTaxBranch = $"{cont.OwnerAddress} อำเภอ{ampher.AmphorName} จังหวัด{province.ProvinceNameTh} {cont.OwnerZipCode}";
          booking.CusSellCode = __company.CustomerCode;
          booking.CusSellName = $"{__company.CustomerPrename} {__company.CustomerName}";

          var zoneId = ctx.Branch.Where(b => b.BranchId == cont.BranchId).Select(b => b.ZoneId).FirstOrDefault();

          // cont.ContractOwner = __company.CustomerCode;
          cont.BranchId = cont.BranchId == null ? booking.BranchId : cont.BranchId;
          cont.AreaPayment = cont.AreaPayment == null ? booking.BranchId : cont.AreaPayment;
          cont.ContractPoint = cont.ContractPoint == null ? zoneId : cont.ContractPoint;
          cont.ContractGroup = cont.ContractGroup == null ? __branch.ContractGroupCode : cont.ContractGroup;
          cont.ContractType = cont.ContractType == null ? __branch.ContractTypeCode : cont.ContractType;
          cont.CreatedBy = cont.CreatedBy == null ? booking.CreateBy : cont.CreatedBy;
          cont.CheckedBy = cont.CheckedBy == null ? booking.CreateBy : cont.CheckedBy;
          cont.KeeperBy = cont.KeeperBy == null ? booking.CreateBy : cont.KeeperBy;
          cont.ApprovedBy = cont.ApprovedBy == null ? booking.CreateBy : cont.ApprovedBy;
        }

                var userDropdown = iUserService.GetDropdowns();

                var contractGroupDropdown = iContGroupService.GetDropdowns();

                var contractTypeDropdown = iContTypeService.GetDropdowns();

                var zoneDropdown = iZoneService.GetDropdowns();

                var branchDropdown = iBranchService.GetDropdowns();

                var statusDropdown = iStatusService.GetDropdown();

        var obj = new Dictionary<string, object>
        {
          {"statusDesc", statusDesc},
          {"creditContract", cont},
          {"creditContractItem", contItem},
          {"sale", calcu},
          {"booking", booking},
          {"statusDropdown", statusDropdown},
          {"userDropdown", userDropdown},
          {"contractGroupDropdown", contractGroupDropdown},
          {"contractTypeDropdown", contractTypeDropdown},
          {"zoneDropdown", zoneDropdown},
          {"branchDropdown", branchDropdown}
        };

                return Ok(obj);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("CreditContractDetail")]
        public IActionResult CreditContractDetails(int contractId)
        {
            return Ok(GetCreditContractDetail(contractId));
        }

        private CreditContractDetail GetCreditContractDetail(int contractId)
        {
            var detail = (from db in ctx.CreditContract

                          join _branch in ctx.Branch on db.BranchId equals _branch.BranchId into a1
                          from branch in a1.DefaultIfEmpty()

                          join _contractType in ctx.MContractType on db.ContractType equals _contractType.TypeCode into a2
                          from contractType in a2.DefaultIfEmpty()

                          join _areaPayment in ctx.Branch on db.AreaPayment equals _areaPayment.BranchId into a3
                          from areaPayment in a3.DefaultIfEmpty()

                          join _contractGroup in ctx.MContractGroup on db.ContractGroup equals _contractGroup.GroupCode into a4
                          from contractGroup in a4.DefaultIfEmpty()

                          join _status in ctx.MStatus on db.ContractStatus equals _status.Id into a5
                          from status in a5.DefaultIfEmpty()

                          join _contractPoint in ctx.Zone on db.ContractPoint equals _contractPoint.ZoneId into a6
                          from contractPoint in a6.DefaultIfEmpty()

                          join _contrachHire in ctx.MCustomer on db.ContractHire equals _contrachHire.CustomerCode into a7
                          from contrachHire in a7.DefaultIfEmpty()

                          join _contractMate in ctx.MCustomer on db.ContractMate equals _contractMate.CustomerCode into a8
                          from contractMate in a8.DefaultIfEmpty()

                          join _contractBooking in ctx.MCustomer on db.ContractBooking equals _contractBooking.CustomerCode into a9
                          from contractBooking in a9.DefaultIfEmpty()

                        join _created in ctx.User on db.CreatedBy equals _created.Id into a14
                        from created in a14.DefaultIfEmpty()

                          join _checked in ctx.User on db.CreatedBy equals _checked.Id into a15
                          from checkedBy in a15.DefaultIfEmpty()

                          join _approve in ctx.User on db.ApprovedBy equals _approve.Id into a16
                          from approve in a16.DefaultIfEmpty()

                          join _keeper in ctx.User on db.KeeperBy equals _keeper.Id into a17
                          from keeper in a17.DefaultIfEmpty()

                          where db.ContractId == contractId

                          select new CreditContractDetail
                          {
                              ContractId = contractId,
                              ContractNo = db.ContractNo,
                              ContractType = contractType.TypeDesc,
                              ContractDate = db.ContractDate,
                              AreaPayment = areaPayment.BranchName,
                              ContractPoint = contractPoint.ZoneName,
                              ContractGroup = contractGroup.GroupDesc,
                              ContractHireNo = contrachHire.CustomerCode,
                              ContractHire = $"{contrachHire.CustomerPrename}{contrachHire.CustomerName} {contrachHire.CustomerSurname}",
                              ContractMate = $"{contractMate.CustomerPrename}{contractMate.CustomerName} {contractMate.CustomerSurname}",
                              ContractBooking = $"{contractBooking.CustomerPrename}{contractBooking.CustomerName} {contractBooking.CustomerSurname}",
                              ContractGurantorNo1 = gurantor1.CustomerCode,
                              ContractGurantor1 = $"{gurantor1.CustomerPrename}{gurantor1.CustomerName} {gurantor1.CustomerSurname}",
                              GurantorRelation1 = db.GurantorRelation1,
                              ContractGurantorNo2 = gurantor2.CustomerCode,
                              ContractGurantor2 = $"{gurantor2.CustomerPrename}{gurantor2.CustomerName} {gurantor2.CustomerSurname}",
                              GurantorRelation2 = db.GurantorRelation2,
                              CreatedBy = created.FullName,
                              CheckedBy = checkedBy.FullName,
                              ApprovedBy = approve.FullName,
                              KeeperBy = keeper.FullName,
                              StatusDesc = status.StatusDesc,
                              Remark = db.Remark

                          }).SingleOrDefault();

            return detail;
        }

        [HttpGet("Detail")]
        public IActionResult Detail(int contractId)
        {
            try
            {
                var detail = GetCreditContractDetail(contractId);

                CreditContract contract = ctx.CreditContract.Where(o => o.ContractId == contractId).SingleOrDefault();

                var __branch = ctx.Branch.SingleOrDefault(x => x.BranchId == 1);

                var __company = ctx.Company.FirstOrDefault(x => x.ComId == 1);

                var booking = iBookService.GetBookingById(contract.BookingId);
                booking.CusTaxNo = __branch.BranchRegisterNo;
                booking.CusTaxBranch = __branch.BranchName;
                booking.CusSellName = __company.ComName;


        var calculate = ctx.Sale.Where(p => p.SaleId == contract.SaleId).SingleOrDefault();

                var statusDropdown = iStatusService.GetDropdown();
                //statusDropdown = statusDropdown.Where(x => x.Value == "27" && x.Value == "33").ToArray();

                //var contItem = ctx.CreditContractItem
                //   .Where(p => p.ContractId == contract.ContractId && p.RefNo == contract.RefNo)
                //   .OrderBy(o => o.InstalmentNo).ToList();

                var outstanding = SetOutstanding(contractId.ToString());

                var delayedInterest = SetDelayedInterest(contractId.ToString());

                var discounts = SetDiscounts(contractId.ToString());

                var cutOffSale = SetCutOffSale(contractId.ToString());

                var historyPayment = SetHistoryPayment(contractId.ToString());

                var obj = new Dictionary<string, object>
                {
                    {"Sale", calculate},
                    {"statusDropdown", statusDropdown},
                    {"creditContractDetail", detail},
                    {"booking", booking},
                    {"outstanding", outstanding },
                    {"delayedInterest", delayedInterest },
                    {"discounts", discounts },
                    {"cutOffSale", cutOffSale },
                    {"historyPayment", historyPayment }
                };

                return Ok(obj);

            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return StatusCode(500, ex.Message);
            }
        }

        private Outstandings SetOutstanding(string contractId)
        {
          var creditContract = c.contract;
          // var _booking = c.booking;
          // Contract
          creditContract.UpdateDate = DateTime.Now;
          // อยู่ระหว่างผ่อนชำระ
          creditContract.ContractStatus = 31;
          ctx.Update(creditContract);
          ctx.SaveChanges();

          transaction.Commit();

          return Ok(creditContract);

        }

        private List<DelayedInterest> SetDelayedInterest(string contractId)
        {
            var list = ctx.DelayedInterest
                .FromSql("sp_RptDelayedInterest @p0", parameters: new[] { contractId })
                .ToList();
            return list;
        }

        private IEnumerable<Discounts> SetDiscounts(string contractId)
        {
          var creditContract = c.contract;
          var _booking = c.booking;
          // Contract
          // ถ้าสัญญา ยังเป็นสัญญาใหม่ จะถูกเปลี่ยนให้เป็น อยู่ระหว่างการผ่อนชำระอัตโนมัติ
          creditContract.ContractStatus = creditContract.ContractStatus == 32 ? 31 : creditContract.ContractStatus;
          creditContract.UpdateDate = DateTime.Now;
          ctx.Update(creditContract);
          ctx.SaveChanges();

          // Calculate
          // Sale calculate = new Sale();
          // calculate = ctx.Sale.SingleOrDefault(o => o.SaleId == creditContract.SaleId);

          // Booking
          // Models.Booking booking = new Models.Booking();
          // booking = ctx.Booking.SingleOrDefault(b => b.BookingId == creditContract.BookingId);

          // // ค้นหาชื่อเช่าซื้อด้วยรหัส
          // var customer = iCustService.GetCustomerByCode(creditContract.ContractHire);

          // if (booking.SellDate == null)
          //   booking.SellDate = DateTime.Now;

          // update booking ให้เป็นสถานะขาย
          // booking.BookingStatus = 2;
          // booking.PaymentPrice = calculate.DepositPrice;
          // booking.PaymentType = booking.BookingDepositType;
          // booking.CusSellName = _booking.CusSellName;
          // booking.CusTaxNo = _booking.CusTaxNo;
          // booking.CusTaxBranch = _booking.CusTaxBranch;
          // booking.SellRemark = _booking.SellRemark;

          // booking.SellBy = creditContract.CreateBy;
          // booking.LStartDate = calculate.FirstPayment.ToString();
          // booking.LPayDay = calculate.DueDate;
          // booking.LTerm = calculate.InstalmentEnd;
          // booking.LInterest = calculate.Interest;
          // booking.LPriceTerm = calculate.InstalmentPrice;

          // if (booking.SellNo == null)
          //   booking.SellNo = iSysParamService.GenerateSellNo((int)creditContract.BranchId);

          // if (booking.VatNo == null)
          //   booking.VatNo = iSysParamService.GenerateVatNo((int)creditContract.BranchId);

          // booking.VatDate = DateTime.Now;
          // booking.VatBy = creditContract.CreateBy;
          // ctx.Update(booking);
          // ctx.SaveChanges();

          transaction.Commit();

          return Ok(creditContract);

        }

        private CutOffSale SetCutOffSale(string contractId)
        {
            var obj = ctx.CutOffSale
                .FromSql("sp_RptCutOffSale @p0", parameters: new[] { contractId })
                .FirstOrDefault();
            return obj;
        }

        private HistoryPayment SetHistoryPayment(string contractId)
        {
            var obj = ctx.HistoryPayment
                .FromSql("sp_RptHistoryPayment @p0", parameters: new[] { contractId })
                .FirstOrDefault();
            return obj;
        }

          // var bk = ctx.Booking.SingleOrDefault(x => x.BookingId == cc.BookingId);
          // bk.BookingStatus = 9;
          // bk.CancelRemark = "ยกเลิกสัญญา";
          // bk.UpdateBy = c.UpdateBy;
          // bk.UpdateDate = DateTime.Now;

                    transaction.Commit();

                    return Ok(creditContract);

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, ex.Message);
                }
            }
        }

        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] Contract c)
        {
            using (var transaction = ctx.Database.BeginTransaction())
            {
                try
                {
                    var creditContract = c.contract;
                    var _booking = c.booking;
                    // Contract
                    // ถ้าสัญญา ยังเป็นสัญญาใหม่ จะถูกเปลี่ยนให้เป็น อยู่ระหว่างการผ่อนชำระอัตโนมัติ
                    creditContract.ContractStatus = creditContract.ContractStatus == 32 ? 31 : creditContract.ContractStatus;
                    creditContract.UpdateDate = DateTime.Now;
                    ctx.Update(creditContract);
                    ctx.SaveChanges();

                    // Calculate
                    Sale calculate = new Sale();
                    calculate = ctx.Sale.SingleOrDefault(o => o.SaleId == creditContract.SaleId);

                    // Booking
                    Models.Booking booking = new Models.Booking();
                    booking = ctx.Booking.SingleOrDefault(b => b.BookingId == creditContract.BookingId);

                    // // ค้นหาชื่อเช่าซื้อด้วยรหัส
                    // var customer = iCustService.GetCustomerByCode(creditContract.ContractHire);

                    if (booking.SellDate == null)
                        booking.SellDate = DateTime.Now;

                    // update booking ให้เป็นสถานะขาย
                    booking.BookingStatus = 2;
                    booking.PaymentPrice = calculate.DepositPrice;
                    booking.PaymentType = booking.BookingDepositType;
                    booking.CusSellName = _booking.CusSellName;
                    booking.CusTaxNo = _booking.CusTaxNo;
                    booking.CusTaxBranch = _booking.CusTaxBranch;
                    booking.SellRemark = _booking.SellRemark;

                    booking.SellBy = creditContract.CreateBy;
                    booking.LStartDate = calculate.FirstPayment.ToString();
                    booking.LPayDay = calculate.DueDate;
                    booking.LTerm = calculate.InstalmentEnd;
                    booking.LInterest = calculate.Interest;
                    booking.LPriceTerm = calculate.InstalmentPrice;

                    if (booking.SellNo == null)
                        booking.SellNo = iSysParamService.GenerateSellNo((int)creditContract.BranchId);

                    if (booking.VatNo == null)
                        booking.VatNo = iSysParamService.GenerateVatNo((int)creditContract.BranchId);

                    booking.VatDate = DateTime.Now;
                    booking.VatBy = creditContract.CreateBy;
                    ctx.Update(booking);
                    ctx.SaveChanges();

                    transaction.Commit();

                    return Ok(creditContract);

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, ex.Message);
                }
            }
        }

        [HttpPost("ContractTermination")]
        public IActionResult ContractTermination([FromBody] ContractTerminate c)
        {

            using (var transaction = ctx.Database.BeginTransaction())
            {
                try
                {
                    var cc = ctx.CreditContract.SingleOrDefault(x => x.ContractId == c.ContractId);

                    // ยกเลิกสัญญา
                    cc.ContractStatus = 33;
                    cc.Remark = c.Remark;
                    cc.UpdateBy = c.UpdateBy;
                    cc.UpdateDate = DateTime.Now;

                    var bk = ctx.Booking.SingleOrDefault(x => x.BookingId == cc.BookingId);
                    bk.BookingStatus = 9;
                    bk.CancelRemark = "ยกเลิกสัญญา";
                    bk.UpdateBy = c.UpdateBy;
                    bk.UpdateDate = DateTime.Now;

                    ctx.SaveChanges();
          // var bk = ctx.Booking.SingleOrDefault(x => x.BookingId == cc.BookingId);
          // bk.BookingStatus = 9;
          // bk.CancelRemark = "ยกเลิกสัญญา";
          // bk.UpdateBy = c.UpdateBy;
          // bk.UpdateDate = DateTime.Now;

                    transaction.Commit();

                    return Ok();

                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    return StatusCode(500, ex.Message);
                }
            }
        }

        public class Contract
        {
            public CreditContract contract { get; set; }
            public Models.Booking booking { get; set; }
        }

        public class ContractTerminate
        {
            public int ContractId { get; set; }
            public string Remark { get; set; }
            public int UpdateBy { get; set; }
        }

        public class CreditContractDetail
        {
            public int ContractId { get; set; }
            public string ContractNo { get; set; }
            public string ContractType { get; set; }
            public DateTime? ContractDate { get; set; }
            public string AreaPayment { get; set; }
            public string ContractPoint { get; set; }
            public string ContractGroup { get; set; }
            public string ContractHireNo { get; set; }
            public string ContractHire { get; set; }
            public string ContractMate { get; set; }
            public string ContractBooking { get; set; }
            public string ContractGurantorNo1 { get; set; }
            public string ContractGurantor1 { get; set; }
            public string GurantorRelation1 { get; set; }
            public string ContractGurantorNo2 { get; set; }
            public string ContractGurantor2 { get; set; }
            public string GurantorRelation2 { get; set; }
            public string CreatedBy { get; set; }
            public string CheckedBy { get; set; }
            public string ApprovedBy { get; set; }
            public string KeeperBy { get; set; }
            public string StatusDesc { get; set; }
            public string Remark { get; set; }
        }
    }
}