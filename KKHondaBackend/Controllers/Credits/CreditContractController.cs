using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using KKHondaBackend.Services;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KKHondaBackend.Controllers.Credits
{
    [Route("api/Credit/Contract")]
    public class CreditContractController : Controller
    {

        private readonly dbwebContext ctx;
        private readonly IBookingServices iBookService;
        private readonly IUserServices iUserService;
        private readonly ICustomerServices iCustService;
        private readonly IRelationService iRelaService;
        private readonly IContractGroupService iContGroupService;
        private readonly IContractTypeService iContTypeService;
        private readonly IZoneService iZoneService;
        private readonly IBranchService iBranchService;
        private readonly ISysParameterService iSysParamService;
        private readonly IStatusService iStatusService;

        public CreditContractController(
            dbwebContext context,
            IBookingServices ibookService,
            IUserServices iuserService,
            ICustomerServices icustService,
            IRelationService irelaService,
            IContractGroupService icontGroupService,
            IContractTypeService iconTypeService,
            IZoneService izoneService,
            IBranchService ibranchService,
            ISysParameterService isysParamService,
            IStatusService istatusService
        )
        {
            ctx = context;
            iBookService = ibookService;
            iUserService = iuserService;
            iCustService = icustService;
            iRelaService = irelaService;
            iContGroupService = icontGroupService;
            iContTypeService = iconTypeService;
            iZoneService = izoneService;
            iBranchService = ibranchService;
            iSysParamService = isysParamService;
            iStatusService = istatusService;
        }

        [HttpGet("Canceled")]
        public IActionResult Canceled()
        {
            List<CreditContractList> creditContractLists = GetListContracts();
            creditContractLists = creditContractLists.Where(o => o.ContractStatus == 0).ToList();
            return Ok(creditContractLists);
        }

        [HttpGet("Active")]
        public IActionResult Active()
        {
            List<CreditContractList> creditContractLists = GetListContracts();
            creditContractLists = creditContractLists.Where(o => o.ContractStatus != 0).ToList();
            return Ok(creditContractLists);
        }

        public List<CreditContractList> GetListContracts()
        {
            var contract = (from db in ctx.CreditContract

                            join _branch in ctx.Branch on db.BranchId equals _branch.BranchId into a1
                            from branch in a1.DefaultIfEmpty()

                            join _contractType in ctx.MContractType on db.ContractType equals _contractType.Id into a2
                            from contractType in a2.DefaultIfEmpty()

                            join _areaPayment in ctx.Branch on db.AreaPayment equals _areaPayment.BranchId into a3
                            from areaPayment in a3.DefaultIfEmpty()

                            join _contractGroup in ctx.MContractGroup on db.ContractGroup equals _contractGroup.Id into a4
                            from contractGroup in a4.DefaultIfEmpty()

                            join _status in ctx.MStatus on db.ContractStatus equals _status.Id into a13
                            from status in a13.DefaultIfEmpty()

                            join _contractPoint in ctx.Zone on db.ContractPoint equals _contractPoint.ZoneId into a6
                            from contractPoint in a6.DefaultIfEmpty()

                            select new CreditContractList
                            {
                                ContractId = db.ContractId,
                                CalculateId = db.CalculateId,
                                Branch = branch.BranchName,
                                ContractNo = db.ContractNo,
                                ContractType = contractType.TypeDesc,
                                ContractDate = db.ContractDate,
                                AreaPayment = areaPayment.BranchName,
                                ContractPoint = contractPoint.ZoneName,
                                ContractGroup = contractGroup.GroupDesc,
                                StatusDesc = status.StatusDesc,
                                ContractStatus = db.ContractStatus,
                                RefNo = db.RefNo,
                            }).ToList();

            return contract;

        }

        // GET api/values/5
        [HttpGet("GetById")]
        public IActionResult Get(int id)
        {
            try
            {
                var cont = ctx.CreditContract.Where(p => p.ContractId == id).SingleOrDefault();

                //var statusDesc = ctx.MStatus
                //.Where(o => o.Id == cont.ContractStatus)
                //.Select(o => o.StatusDesc)
                //.SingleOrDefault();

                var contItem = ctx.CreditContractItem
                    .Where(p => p.ContractId == id && p.RefNo == cont.RefNo)
                    .OrderBy(o => o.InstalmentNo).ToList();

                var calcu = ctx.CreditCalculate.Where(p => p.CalculateId == cont.CalculateId).SingleOrDefault();

                var booking = iBookService.GetBookingById(cont.BookingId);

                var zoneId = ctx.Branch
                    .Where(b => b.BranchId == booking.BranchId)
                    .Select(b => b.ZoneId)
                    .SingleOrDefault();


                cont.BranchId = cont.BranchId == null ? booking.BranchId : cont.BranchId;
                cont.AreaPayment = cont.AreaPayment == null ? booking.BranchId : cont.AreaPayment;
                cont.ContractPoint = cont.ContractPoint == null ? zoneId : cont.ContractPoint;

                cont.CreatedBy = cont.CreatedBy == null ? booking.CreateBy : cont.CreatedBy;
                cont.CheckedBy = cont.CheckedBy == null ? booking.CreateBy : cont.CheckedBy;
                cont.KeeperBy = cont.KeeperBy == null ? booking.CreateBy : cont.KeeperBy;
                cont.ApprovedBy = cont.ApprovedBy == null ? booking.CreateBy : cont.ApprovedBy;

                var userDropdown = iUserService.GetDropdowns();

                var customerDropdown = iCustService.GetDropdowns();

                var contractMateDropdown = customerDropdown;
                if (cont.ContractMate != null)
                {
                    var mateDd = iCustService.GetDropdownByKey(cont.ContractMate);
                    contractMateDropdown = contractMateDropdown.Concat(mateDd).ToArray();
                }

                var contractUserDropdown = customerDropdown;
                if (cont.ContractUser != null)
                {
                    var userDd = iCustService.GetDropdownByKey(cont.ContractUser);
                    contractUserDropdown = contractUserDropdown.Concat(userDd).ToArray();
                }

                var contractGurantor1Dropdown = customerDropdown;
                if (cont.ContractGurantor1 != null)
                {
                    var gurantorDd = iCustService.GetDropdownByKey(cont.ContractGurantor1);
                    contractGurantor1Dropdown = contractGurantor1Dropdown.Concat(gurantorDd).ToArray();
                }

                var contractGurantor2Dropdown = customerDropdown;
                if (cont.ContractGurantor2 != null)
                {
                    var gurantorDd = iCustService.GetDropdownByKey(cont.ContractGurantor2);
                    contractGurantor2Dropdown = contractGurantor2Dropdown.Concat(gurantorDd).ToArray();
                }

                var relationDropdown = iRelaService.GetDropdowns();

                var contractGroupDropdown = iContGroupService.GetDropdowns();

                var contractTypeDropdown = iContTypeService.GetDropdowns();

                var zoneDropdown = iZoneService.GetDropdowns();

                var branchDropdown = iBranchService.GetDropdowns();

                var statusDropdown = iStatusService.GetDropdown();

                var obj = new Dictionary<string, object>
                {
                    {"statusDropdown", statusDropdown},
                    {"creditContract", cont},
                    {"creditContractItem", contItem},
                    {"creditCalculate", calcu},
                    {"booking", booking},
                    {"userDropdown", userDropdown},
                    {"contractMateDropdown", contractMateDropdown},
                    {"contractUserDropdown", contractUserDropdown},
                    {"contractGurantor1Dropdown", contractGurantor1Dropdown},
                    {"contractGurantor2Dropdown", contractGurantor2Dropdown},
                    {"relationDropdown", relationDropdown},
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

        [HttpGet("Detail")]
        public IActionResult Detail(int contractId)
        {
            try
            {

                var detail = (from db in ctx.CreditContract

                              join _branch in ctx.Branch on db.BranchId equals _branch.BranchId into a1
                              from branch in a1.DefaultIfEmpty()

                              join _contractType in ctx.MContractType on db.ContractType equals _contractType.Id into a2
                              from contractType in a2.DefaultIfEmpty()

                              join _areaPayment in ctx.Branch on db.AreaPayment equals _areaPayment.BranchId into a3
                              from areaPayment in a3.DefaultIfEmpty()

                              join _contractGroup in ctx.MContractGroup on db.ContractGroup equals _contractGroup.Id into a4
                              from contractGroup in a4.DefaultIfEmpty()

                              join _status in ctx.MStatus on db.ContractStatus equals _status.Id into a5
                              from status in a5.DefaultIfEmpty()

                              join _contractPoint in ctx.Zone on db.ContractPoint equals _contractPoint.ZoneId into a6
                              from contractPoint in a6.DefaultIfEmpty()

                              join _contrachHire in ctx.MCustomer on db.ContractHire equals _contrachHire.CustomerCode into a7
                              from contrachHire in a7.DefaultIfEmpty()

                              join _contractMate in ctx.MCustomer on db.ContractMate equals _contractMate.CustomerCode into a8
                              from contractMate in a8.DefaultIfEmpty()

                              join _contractUser in ctx.MCustomer on db.ContractUser equals _contractUser.CustomerCode into a9
                              from contractUser in a9.DefaultIfEmpty()

                              join _gurantor1 in ctx.MCustomer on db.ContractGurantor1 equals _gurantor1.CustomerCode into a10
                              from gurantor1 in a10.DefaultIfEmpty()

                              join _gurantor2 in ctx.MCustomer on db.ContractGurantor2 equals _gurantor2.CustomerCode into a11
                              from gurantor2 in a11.DefaultIfEmpty()

                              join _relation1 in ctx.MRelation on db.GurantorRelation1 equals _relation1.Id into a12
                              from relation1 in a12.DefaultIfEmpty()

                              join _relation2 in ctx.MRelation on db.GurantorRelation2 equals _relation2.Id into a13
                              from relation2 in a13.DefaultIfEmpty()

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
                                  ContractHire = contrachHire.CustomerPrename + contrachHire.CustomerName + " " + contrachHire.CustomerSurname,
                                  ContractMate = contractMate.CustomerPrename + contractMate.CustomerName + " " + contractMate.CustomerSurname,
                                  ContractUser = contractUser.CustomerPrename + contractUser.CustomerName + " " + contractUser.CustomerSurname,
                                  ContractGurantor1 = gurantor1.CustomerPrename + gurantor1.CustomerName + " " + gurantor1.CustomerSurname,
                                  GurantorRelation1 = relation1.RelationDesc,
                                  ContractGurantor2 = gurantor2.CustomerPrename + gurantor2.CustomerName + " " + gurantor2.CustomerSurname,
                                  GurantorRelation2 = relation2.RelationDesc,
                                  CreatedBy = created.Fullname,
                                  CheckedBy = checkedBy.Fullname,
                                  ApprovedBy = approve.Fullname,
                                  KeeperBy = keeper.Fullname,
                                  StatusDesc = status.StatusDesc,
                                  Remark = db.Remark

                              }).SingleOrDefault();

                CreditContract contract = ctx.CreditContract.Where(o => o.ContractId == contractId).SingleOrDefault();

                var booking = iBookService.GetBookingById(contract.BookingId);

                var calculate = ctx.CreditCalculate.Where(p => p.CalculateId == contract.CalculateId).SingleOrDefault();

                //var contItem = ctx.CreditContractItem
                //   .Where(p => p.ContractId == contract.ContractId && p.RefNo == contract.RefNo)
                //   .OrderBy(o => o.InstalmentNo).ToList();

                var outstanding = SetOutstanding(contractId, contract.RefNo);

                var delayedInterest = SetDelayedInterest(contractId, contract.RefNo);

                var discounts = SetDiscounts(contractId, contract.RefNo);

                var cutOffSale = SetCutOffSale(contractId, contract.RefNo);

                var historyPayment = SetHistoryPayment(contractId, calculate.CalculateId, contract.ContractNo, contract.RefNo);

                var obj = new Dictionary<string, object>
                {
                    {"creditCalculate", calculate},
                    //{"creditContractItem", contItem},
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
                return NotFound();
            }
        }

        private Outstandings SetOutstanding(int contractId, string refNo)
        {
            var contractItem = ctx.CreditContractItem.Where(item => item.ContractId == contractId && item.RefNo == refNo).ToList();
            // ดอกเบี้ยค้างชำระ
            var fineSumeOut = (from db in contractItem
                               where (db.FineSumStatus == 0 || db.FineSumStatus == null)
                               group db by db.ContractId into a1
                               select new Outstandings
                               {
                                   FineSume = (int)a1.Sum(x => x.FineSum == null ? 0 : x.FineSum)
                               }).FirstOrDefault();

            // เงินดาวน์ค้างชำระ
            var depositOut = contractItem
                .Where(item => (item.PayNetPrice == null || item.PayNetPrice == 0) && item.InstalmentNo == 0)
                .Select(item => new Outstandings { Deposit = (decimal)item.BalanceNetPrice })
                .FirstOrDefault();

            var dateNow = DateTime.Now.Date;

            // ค่างวดค้างชำระ
            var balanceOut = (from db in contractItem
                              where (db.PayNetPrice == null || db.PayNetPrice == 0) &&
                              db.InstalmentNo > 0 && DateTime.Parse(db.DueDate.ToString()).Date <= dateNow
                              group db by db.ContractId into a1
                              select new Outstandings
                              {
                                  Balance = (decimal)a1.Sum(x => x.BalanceNetPrice),
                                  StartInstalment = a1.Min(x => x.InstalmentNo),
                                  EndInstalment = a1.Max(x => x.InstalmentNo)
                              }).FirstOrDefault();

            // งวดต่อไปที่ต้องชำระ
            var nextInstalment = (from db in contractItem
                                  where db.InstalmentNo > 0 && DateTime.Parse(db.DueDate.ToString()).Date > dateNow
                                  group db by new
                                  {
                                      db.ContractId,
                                      db.BalanceNetPrice
                                  } into g
                                  select new Outstandings
                                  {
                                      NextInstalment = g.Min(x => x.InstalmentNo),
                                      NextDueDate = (DateTime)g.Min(x => x.DueDate),
                                      NextInstalmentBalance = (decimal)g.Key.BalanceNetPrice
                                  }).FirstOrDefault();

            // ค่างวดที่ยังไม่ถึงกำหนด
            var futureInstalment = (from db in contractItem
                                    where db.InstalmentNo > 0 && DateTime.Parse(db.DueDate.ToString()).Date > dateNow
                                    group db by db.ContractId into g
                                    select new Outstandings
                                    {
                                        FutureInstalment = g.Count(),
                                        FutureInstalmentBalance = (decimal)g.Sum(x => x.BalanceNetPrice)
                                    }).FirstOrDefault();

            // ยอดค้างชำระทั้งสิ้น
            var outstandingTotal = fineSumeOut.FineSume + depositOut.Deposit + balanceOut.Balance;

            // ยอดที่รับชำระแล้ว
            var payPriceTotal = (from db in contractItem
                                 where db.PayNetPrice != null || db.PayNetPrice > 0
                                 group db by db.ContractId into g
                                 select new
                                 {
                                     payPrice = g.Sum(x => x.PayNetPrice == null ? 0 : (decimal)x.PayNetPrice)
                                 }).FirstOrDefault();

            var obj = new Outstandings
            {
                FineSume = fineSumeOut.FineSume,
                Deposit = depositOut.Deposit,
                Balance = balanceOut.Balance,
                StartInstalment = balanceOut.StartInstalment,
                EndInstalment = balanceOut.EndInstalment,
                OutstandingTotal = outstandingTotal,
                PayPriceTotal = payPriceTotal.payPrice,
                NextInstalment = nextInstalment.NextInstalment,
                NextInstalmentBalance = nextInstalment.NextInstalmentBalance,
                NextDueDate = nextInstalment.NextDueDate,
                FutureInstalment = futureInstalment.FutureInstalment,
                FutureInstalmentBalance = futureInstalment.FutureInstalmentBalance,
            };
            return obj;

        }

        private List<DelayedInterest> SetDelayedInterest(int contractId, string refNo)
        {
            var list = ctx.CreditContractItem
                .Where(p => p.ContractId == contractId && p.RefNo == refNo && p.InstalmentNo > 0)
                .Select(p => new DelayedInterest
                {
                    InstalmentNo = p.InstalmentNo,
                    DueDate = (DateTime)p.DueDate,
                    Balance = (decimal)p.BalanceNetPrice,
                    FineSum = p.FineSum == null ? 0 : (decimal)p.FineSum,
                    Outstanding = (decimal)p.BalanceNetPrice + p.FineSum == null ? 0 : (decimal)p.FineSum,
                    DelayDueDate = p.DelayDueDate == null ? 0 : (int)p.DelayDueDate,
                    Remark = p.Remark
                }).ToList();

            return list;
        }

        private List<Discounts> SetDiscounts(int contractId, string refNo)
        {
            var list = ctx.CreditContractItem
                .Where(p => p.ContractId == contractId && p.RefNo == refNo && p.InstalmentNo > 0)
                .Select(p => new Discounts
                {                    
                    InstalmentNo = p.InstalmentNo,
                    DueDate = (DateTime)p.DueDate,
                    Balance = (decimal)p.BalanceNetPrice,
                    Outstanding = (decimal)p.BalanceNetPrice ,
                    Discount = (decimal)p.DiscountPrice
                }).ToList();

            return list;
        }

        private CutOffSale SetCutOffSale(int contractId, string refNo)
        {
            var contractItem = ctx.CreditContractItem.Where(p => p.ContractId == contractId && p.RefNo == refNo).ToList();
            var goodPrice = (from db in contractItem
                             where db.PayNetPrice == null
                             group db by db.ContractId into g
                             select new
                             {
                                 InterestInstalment = g.Sum(x => (decimal)x.InterestInstalment),
                                 GoodPrice = g.Sum(x => (decimal)x.GoodsPrice)
                             }).FirstOrDefault();

            // ค่างวดที่ต้องชำระ
            var balance = (from db in contractItem
                           where db.PayNetPrice == null
                           group db by db.ContractId into g
                           select new
                           {
                               Balance = g.Sum(x => (decimal)x.BalanceNetPrice)
                           }).FirstOrDefault();

            // เงินดาวน์
            var deposit = contractItem
                .Where(db => db.InstalmentNo == 0 && db.PayNetPrice == null)
                .Select(p => new { Deposit = (decimal)p.BalanceNetPrice })
                .FirstOrDefault();

            // ดอกเบี้ยล่าช้า -- ที่ยังไม่จ่าย
            var interest = (from db in contractItem
                            where db.FineSumStatus == null || db.FineSumStatus == 0
                            group db by db.ContractId into g
                            select new
                            {
                                FineSum = g.Sum(x => x.FineSum == null ? 0 : (int)x.FineSum)
                            }).FirstOrDefault();

            // ส่วนลด -- ที่ยังไม่ได้ใช้
            var discount = (from db in contractItem
                            where db.UseDiscount == null || db.UseDiscount == 0
                            group db by db.ContractId into g
                            select new
                            {
                                Discount = g.Sum(x => x.DiscountPrice == null ? 0 : (decimal)x.DiscountPrice)
                            }).FirstOrDefault();

            // ส่วนลดตัดสด -- ที่ยังไม่ได้ใช้
            var distCutOffSale = (from db in contractItem
                                  where db.UseDistCutOffSale == null || db.UseDistCutOffSale == 0
                                  group db by db.ContractId into g
                                  select new
                                  {
                                      Discount = g.Sum(x => x.DistCutOffSalePrice == null ? 0 : (decimal)x.DistCutOffSalePrice)
                                  }).FirstOrDefault();

            var sumBalance = balance.Balance + deposit.Deposit + interest.FineSum;
            var sumDiscount = discount.Discount + distCutOffSale.Discount;
            var totalBalance = sumBalance - sumDiscount;

            var obj = new CutOffSale
            {
                InterestInstalment = goodPrice.InterestInstalment,
                GoodPrice = goodPrice.GoodPrice,
                Balance = balance.Balance,
                FineSum = interest.FineSum,
                SumBalance = sumBalance,
                Discount = discount.Discount,
                DistCutOffSale = distCutOffSale.Discount,
                SumDiscount = sumDiscount,
                TotalBalance = totalBalance
            };

            return obj;
        }

        private HistoryPayment SetHistoryPayment(int contractId, int calculateId, string contractNo, string refNo)
        {
            var dateNow = DateTime.Parse(new DateTime().ToString()).Date;
            var contractItem = ctx.CreditContractItem
                .Where(p => p.ContractId == contractId && p.RefNo == refNo &&
                DateTime.Parse(p.DueDate.ToString()).Date <= dateNow)
                .ToList();
            var calculate = ctx.CreditCalculate
                .Where(p => p.CalculateId == calculateId)
                .FirstOrDefault();

            var payBefore = 0;
            var payLate = 0;
            var paymatch = 0;
            foreach (var a in contractItem)
            {                
                var payday = a.PayDate == null ? dateNow : DateTime.Parse(a.PayDate.ToString()).Date;
                var duedate = DateTime.Parse(a.DueDate.ToString()).Date;
                if (payday > duedate)
                {
                    // จำนวนการชำละล่าช้า
                    payLate += 1;
                }
                else if (payday <= duedate)
                {
                    // จำนวนการชำระก่อน
                    payBefore += 1;
                }
                else if (payday == duedate)
                {
                    // จำนวนการชำระตรงกับวันที่กำหนด
                    paymatch += 1;
                }
            }

            var obj = new HistoryPayment
            {
                ContractNo = contractNo,
                InstalmentEnd = calculate.InstalmentEnd,
                PayBefore = payBefore,
                PayMatch = paymatch,
                PayLate = payLate,
                RateLate = payLate * 100 / calculate.InstalmentEnd,
                Grade = null
            };

            return obj;
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] CreditContract creditContract)
        {
            using (var transaction = ctx.Database.BeginTransaction())
            {
                try
                {
                    // Contract
                    creditContract.UpdateDate = DateTime.Now;
                    creditContract.ContractStatus = 31; // อยู่ระหว่างผ่อนชำระ
                    ctx.Update(creditContract);
                    ctx.SaveChanges();

                    // Calculate
                    CreditCalculate calculate = new CreditCalculate();
                    calculate = ctx.CreditCalculate.SingleOrDefault(o => o.CalculateId == creditContract.CalculateId);

                    // Booking
                    Models.Booking booking = new Models.Booking();
                    booking = ctx.Booking.SingleOrDefault(b => b.BookingId == creditContract.BookingId);

                    // ค้นหาชื่อเช่าซื้อด้วยรหัส
                    var customer = iCustService.GetCustomerByCode(creditContract.ContractHire);

                    booking.SellDate = DateTime.Now;
                    booking.BookingStatus = 2; // สถานะขาย

                    booking.PaymentPrice = calculate.DepositPrice;
                    booking.PaymentType = booking.BookingDepositType;
                    booking.CusSellName = customer.CustomerFullName;
                    booking.CusTaxNo = customer.IdCard;

                    booking.SellBy = creditContract.CreateBy;
                    booking.LStartDate = calculate.FirstPayment.ToString();
                    booking.LPayDay = calculate.DueDate;
                    booking.LTerm = calculate.InstalmentEnd;
                    booking.LInterest = calculate.Interest;
                    booking.SellNo = iSysParamService.GenerateSellNo((int)creditContract.BranchId);
                    booking.LPriceTerm = calculate.InstalmentPrice;
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

        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] CreditContract creditContract)
        {
            using (var transaction = ctx.Database.BeginTransaction())
            {
                try
                {
                    // Contract
                    creditContract.UpdateDate = DateTime.Now;
                    ctx.Update(creditContract);
                    ctx.SaveChanges();

                    // Calculate
                    CreditCalculate calculate = new CreditCalculate();
                    calculate = ctx.CreditCalculate.SingleOrDefault(o => o.CalculateId == creditContract.CalculateId);

                    // Booking
                    Models.Booking booking = new Models.Booking();
                    booking = ctx.Booking.SingleOrDefault(b => b.BookingId == creditContract.BookingId);

                    // ค้นหาชื่อเช่าซื้อด้วยรหัส
                    var customer = iCustService.GetCustomerByCode(creditContract.ContractHire);

                    if (booking.SellDate == null)
                        booking.SellDate = DateTime.Now;

                    booking.BookingStatus = 2; // สถานะขาย
                    booking.PaymentPrice = calculate.DepositPrice;
                    booking.PaymentType = booking.BookingDepositType;
                    booking.CusSellName = customer.CustomerFullName;
                    booking.CusTaxNo = customer.IdCard;

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

        private class Outstandings
        {
            public decimal FineSume { get; set; }
            public decimal Deposit { get; set; }
            public decimal Balance { get; set; }
            public int StartInstalment { get; set; }
            public int EndInstalment { get; set; }
            public decimal OutstandingTotal { get; set; }
            public decimal PayPriceTotal { get; set; }
            public int NextInstalment { get; set; }
            public decimal NextInstalmentBalance { get; set; }
            public DateTime NextDueDate { get; set; }
            public int FutureInstalment { get; set; }
            public decimal FutureInstalmentBalance { get; set; }
        }

        private class DelayedInterest
        {
            public int InstalmentNo { get; set; }
            public DateTime DueDate { get; set; }
            public decimal Balance { get; set; }
            public decimal FineSum { get; set; }
            public decimal Outstanding { get; set; }
            public int DelayDueDate { get; set; }
            public string Remark { get; set; }
        }

        private class Discounts
        {
            public int InstalmentNo { get; set; }
            public DateTime DueDate { get; set; }
            public decimal Balance { get; set; }
            public decimal Outstanding { get; set; }
            public decimal Discount { get; set; }
        }

        private class CutOffSale
        {
            public decimal InterestInstalment { get; set; }
            public decimal GoodPrice { get; set; }
            public decimal Balance { get; set; }
            public decimal FineSum { get; set; }
            public decimal SumBalance { get; set; }
            public decimal Discount { get; set; }
            public decimal DistCutOffSale { get; set; }
            public decimal SumDiscount { get; set; }
            public decimal TotalBalance { get; set; }
        }

        private class HistoryPayment
        {
            public string ContractNo { get; set; }
            public DateTime ContractDate { get; set; }
            public decimal InstalmentEnd { get; set; }
            public int PayBefore { get; set; }
            public int PayMatch { get; set; }
            public int PayLate { get; set; }
            public decimal RateLate { get; set; }
            public string Grade { get; set; }
        }

        public class CreditContractList
        {
            public int ContractId { get; set; }
            public int CalculateId { get; set; }
            public string Branch { get; set; }
            public string ContractNo { get; set; }
            public string ContractType { get; set; }
            public DateTime? ContractDate { get; set; }
            public string AreaPayment { get; set; }
            public string ContractPoint { get; set; }
            public string ContractGroup { get; set; }
            public string ContractHire { get; set; }
            public string ContractUser { get; set; }
            public string ContractGurantor1 { get; set; }
            public string ContractGurantor2 { get; set; }
            public string CreatedBy { get; set; }
            public string CheckedBy { get; set; }
            public string ApprovedBy { get; set; }
            public string KeeperBy { get; set; }
            public string StatusDesc { get; set; }
            public int? ContractStatus { get; set; }
            public string RefNo { get; set; }
            public string CreateBy { get; set; }
            public DateTime CreateDate { get; set; }
            public string UpdateBy { get; set; }
            public DateTime? UpdateDate { get; set; }
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
            public string ContractHire { get; set; }
            public string ContractMate { get; set; }
            public string ContractUser { get; set; }
            public string ContractGurantor1 { get; set; }
            public string GurantorRelation1 { get; set; }
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
