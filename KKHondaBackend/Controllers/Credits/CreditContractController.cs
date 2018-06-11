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

                            join _contractType in ctx.MContractType on db.ContractType equals _contractType.Id into a2
                            from contractType in a2.DefaultIfEmpty()

                            join _zone in ctx.Zone on db.AreaPayment equals _zone.ZoneId into a3
                            from zone in a3.DefaultIfEmpty()

                            join _contractGroup in ctx.MContractGroup on db.ContractGroup equals _contractGroup.Id into a4
                            from contractGroup in a4.DefaultIfEmpty()

                            join _status in ctx.MStatus on db.ContractStatus equals _status.Id into a13
                            from status in a13.DefaultIfEmpty()
                            
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
                                Status = db.ContractStatus,
                                ContractStatus = status.StatusDesc,
                                RefNo = db.RefNo,
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
                var statusText = ctx.MStatus
                                    .Where(o => o.Id == cont.ContractStatus)
                                    .Select(o => o.StatusDesc)
                                    .SingleOrDefault();

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
