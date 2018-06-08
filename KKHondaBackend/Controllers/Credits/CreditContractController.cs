using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using KKHondaBackend.Services;

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

        public CreditContractController(
            dbwebContext context,
            IBookingServices ibookService,
            IUserServices iuserService,
            ICustomerServices icustService,
            IRelationService irelaService,
            IContractGroupService icontGroupService,
            IContractTypeService iconTypeService,
            IZoneService izoneService,
            IBranchService ibranchService
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
        }

        [HttpGet]
        public IActionResult GetAction()
        {
            var contract = (from db in ctx.CreditContract

                            join _branch in ctx.Branch on db.BranchId equals _branch.BranchId into a1
                            from branch in a1.DefaultIfEmpty()

                            join _contractType in ctx.MContractTypes on db.ContractType equals _contractType.Id into a2
                            from contractType in a2.DefaultIfEmpty()

                            join _zone in ctx.Zone on db.AreaPayment equals _zone.ZoneId into a3
                            from zone in a3.DefaultIfEmpty()

                            join _contractGroup in ctx.MContractGroups on db.ContractGroup equals _contractGroup.Id into a4
                            from contractGroup in a4.DefaultIfEmpty()

                            //join _custHire in ctx.MCustomer on db.ContractHire equals _custHire.CustomerCode into a5
                            //from custHire in a5.DefaultIfEmpty()

                            //join _custUser in ctx.MCustomer on db.ContractUser equals _custUser.CustomerCode into a6
                            //from custUser in a6.DefaultIfEmpty()

                            //join _custGura1 in ctx.MCustomer on db.ContractGurantor1 equals _custGura1.CustomerCode into a7
                            //from custGura1 in a7.DefaultIfEmpty()

                            //join _custGura2 in ctx.MCustomer on db.ContractGurantor2 equals _custGura2.CustomerCode into a8
                            //from custGura2 in a8.DefaultIfEmpty()

                            //join _userCreate in ctx.User on db.CreatedBy equals _userCreate.Id into a9
                            //from userCreate in a9.DefaultIfEmpty()

                            //join _userCheck in ctx.User on db.CheckedBy equals _userCheck.Id into a10
                            //from userCheck in a10.DefaultIfEmpty()

                            //join _userApprove in ctx.User on db.ApprovedBy equals _userApprove.Id into a11
                            //from userApprove in a11.DefaultIfEmpty()

                            //join _userKeeper in ctx.User on db.KeeperBy equals _userKeeper.Id into a12
                            //from userKeeper in a12.DefaultIfEmpty()

                            join _status in ctx.MStatuses on db.ContractStatus equals _status.Id into a13
                            from status in a13.DefaultIfEmpty()

                            //join _userCreateBy in ctx.User on db.CreateBy equals _userCreateBy.Id into a14
                            //from userCreateBy in a14.DefaultIfEmpty()

                            //join _userUpdate in ctx.User on db.UpdateBy equals _userUpdate.Id into a15
                            //from userUpdate in a15.DefaultIfEmpty()


                            select new CreditContractList
                            {
                                ContractId = db.ContractId,
                                CalculateId = db.CalculateId,
                                Branch = branch.BranchName,
                                ContractNo = db.ContractNo,
                                ContractType = contractType.TypeDesc,
                                ContractDate = db.ContractDate,
                                AreaPayment = zone.ZoneName,
                                ContractPoint = branch.BranchName,
                                ContractGroup = contractGroup.GroupDesc,
                                //ContractHire =  custHire.CustomerPrename + custHire.CustomerName + " " + custHire.CustomerSurname,
                                //ContractUser =  custUser.CustomerPrename + custUser.CustomerName + " " + custUser.CustomerSurname,
                                //ContractGurantor1 =  custGura1.CustomerPrename + custGura1.CustomerName + " " + custGura1.CustomerSurname,
                                //ContractGurantor2 =  custGura2.CustomerPrename + custGura2.CustomerName + " " + custGura2.CustomerSurname,
                                //CreatedBy = userCreate.Fullname,
                                //CheckedBy = userCheck.Fullname,
                                //ApprovedBy = userApprove.Fullname,
                                //KeeperBy = userKeeper.Fullname,
                                Status = db.ContractStatus,
                                ContractStatus = status.StatusDesc,
                                RefNo = db.RefNo,
                                //CreateBy = userCreateBy.Fullname,
                                //CreateDate = db.CreateDate,
                                //UpdateBy = userUpdate.Fullname,
                                //UpdateDate = db.UpdateDate,
                            }).ToList();

            return Ok(contract);

        }

        // GET api/values/5
        [HttpGet("GetById")]
        public IActionResult Get(int id)
        {
            try
            {
                var cont = ctx.CreditContract.Where(prop => prop.ContractId == id).SingleOrDefault();
                var statusText = ctx.MStatuses
                                    .Where(o => o.Id == cont.ContractStatus)
                                    .Select(o => o.StatusDesc)
                                    .SingleOrDefault();

                var contItem = ctx.CreditContractItem.Where(p => p.ContractId == id).OrderBy(o => o.InstalmentNo).ToList();

                var calcu = ctx.CreditCalculate.Where(p => p.CalculateId == cont.CalculateId).SingleOrDefault();

                var booking = iBookService.GetBookingById(cont.BookingId);

                var userDropdown = iUserService.GetDropdowns();

                var customerDropdown = iCustService.GetDropdowns();

                var relationDropdown = iRelaService.GetDropdowns();

                var contractGroupDropdown = iContGroupService.GetDropdowns();

                var contractTypeDropdown = iContTypeService.GetDropdowns();

                var zoneDropdown = iZoneService.GetDropdowns();

                var branchDropdown = iBranchService.GetDropdowns();

                var obj = new Dictionary<string, object>
                {
                    {"statusText", statusText},
                    {"creditContract", cont},
                    {"creditContractItem", contItem},
                    {"creditCalculate", calcu},
                    {"booking", booking},
                    {"userDropdown", userDropdown},
                    {"customerDropdown", customerDropdown},
                    {"relationDropdown", relationDropdown},
                    {"contractGroupDropdown", contractGroupDropdown},
                    {"contractTypeDropdown", contractTypeDropdown},
                    {"zoneDropdown", zoneDropdown},
                    {"branchDropdown", branchDropdown}
                };

                return Ok(obj);

            } catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] CreditContract creditContract)
        {

            try
            {
                // Contract
                creditContract.UpdateDate = DateTime.Now;
                creditContract.ContractStatus = 31; // อยู่ระหว่างผ่อนชำระ
                ctx.Update(creditContract);
                ctx.SaveChanges();

                return Ok(creditContract);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] CreditContract creditContract) 
        { 

            try
            {
                // Contract
                creditContract.UpdateDate = DateTime.Now;
                ctx.Update(creditContract);
                ctx.SaveChanges();

                return Ok(creditContract);

            }catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }


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
            public int? Status { get; set; }
            public string ContractStatus { get; set; }
            public string RefNo { get; set; }
            public string CreateBy { get; set; }
            public DateTime CreateDate { get; set; }
            public string UpdateBy { get; set; }
            public DateTime? UpdateDate { get; set; }
        }
    }
}
