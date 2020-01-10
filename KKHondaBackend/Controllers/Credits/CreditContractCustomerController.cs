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
using static KKHondaBackend.Controllers.Credits.CreditContractController;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KKHondaBackend.Controllers.Credits
{
    //   [ApiController]
    [Produces("application/json")]
    [Route("api/Credit/ContractCustomer")]
    public class CreditContractCustomerController : Controller
    {

        private readonly dbwebContext ctx;
        private readonly IBookingServices iBookService;
        private readonly IUserServices iUserService;
        private readonly ICustomerServices iCustService;
        private readonly IContractGroupService iContGroupService;
        private readonly IContractTypeService iContTypeService;
        private readonly IZoneService iZoneService;
        private readonly IBranchService iBranchService;
        private readonly ISysParameterService iSysParamService;
        private readonly IStatusService iStatusService;

        public CreditContractCustomerController(
            dbwebContext context,
            IBookingServices ibookService,
            IUserServices iuserService,
            ICustomerServices icustService,
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
            iContGroupService = icontGroupService;
            iContTypeService = iconTypeService;
            iZoneService = izoneService;
            iBranchService = ibranchService;
            iSysParamService = isysParamService;
            iStatusService = istatusService;
        }

        [HttpGet("ContracNoDropdown")]
        public IActionResult ContracNoDropdown(string mode, int contractId)
        {
            var Dropdowns = new List<Dropdown>();
            if (mode == "Edit")
            {
                Dropdowns = ctx.CreditContract
                  .Where(o => o.ContractId == contractId)
                  .Select(o => new Dropdown
                  {
                      Value = o.ContractId.ToString(),
                      Text = o.ContractNo
                  }).ToList();
            }
            else
            {
                var cl = ctx.CreditCollectionLetter.Select(x => x.ContractId).ToList();
                Dropdowns = ctx.CreditContract
                                  .Where(o => o.ContractStatus != 29 && o.ContractStatus != 30 && !cl.Contains(o.ContractId))
                                  .Select(o => new Dropdown
                                  {
                                      Value = o.ContractId.ToString(),
                                      Text = o.ContractNo
                                  }).ToList();
            }

            return Ok(Dropdowns);
        }

        [HttpGet("CheckGuarantor")]
        public IActionResult CheckGuarantor(string bookNo)
        {
            var getConId = ctx.CreditCollectionLetterDetail.FirstOrDefault(x => x.CldBookNo == bookNo);
            var CreditContract = ctx.CreditContract
                  .Where(x => x.ContractId == getConId.ContractId)
                  .Select(o => new CreditCollectionLetterList
                  {
                      ContractGurantor1 = o.ContractGurantor1
                  }).FirstOrDefault();

            return Ok(CreditContract);
        }

        [HttpGet("BookNoDropdown")]
        public IActionResult BookNoDropdown()
        {
            var Dropdowns = ctx.CreditCollectionLetterDetail
                  .Select(o => new Dropdown
                  {
                      Value = o.CldBookNo,
                      Text = o.CldBookNo,
                  }).ToList();

            return Ok(Dropdowns);
        }

        [HttpGet("OperatorDropdown")]
        public IActionResult OperatorDropdown()
        {
            var Dropdowns = ctx.User
                  .Select(o => new Dropdown
                  {
                      Value = o.Id.ToString(),
                      Text = o.FullName,
                  }).ToList();

            return Ok(Dropdowns);
        }

        [HttpGet("StatusDropdown")]
        public IActionResult StatusDropdown()
        {
            var status = new string[] { "14", "15", "16", "19", "21" };
            var Dropdowns = ctx.MStatus
                  .Where(x => status.Contains(x.Id.ToString()))
                  .Select(o => new Dropdown
                  {
                      Value = o.Id.ToString(),
                      Text = o.StatusDesc,
                  })
                  .OrderByDescending(x => x.Value)
                  .ToList();

            return Ok(Dropdowns);
        }

        [HttpGet("SelectDetail")]
        public async Task<IActionResult> SelectDetail(int cldId)
        {
            var detail = await ctx.CreditCollectionLetterDetail.Where(x => x.CldId == cldId).FirstOrDefaultAsync();
            return Ok(detail);
        }

        [HttpGet("GetCustomerContractList")]
        public async Task<IActionResult> GetCustomerContractList()
        {
            var list = await (from db in ctx.CreditContract
                              join cl in ctx.CreditCollectionLetter on db.ContractId equals cl.ContractId

                              join _status in ctx.MStatus on db.ContractStatus equals _status.Id into a1
                              from status in a1.DefaultIfEmpty()

                              join _contrachHire in ctx.MCustomer on db.ContractHire equals _contrachHire.CustomerCode into a2
                              from contrachHire in a2.DefaultIfEmpty()

                              join _gurantor1 in ctx.MCustomer on db.ContractGurantor1 equals _gurantor1.CustomerCode into a3
                              from gurantor1 in a3.DefaultIfEmpty()

                              join _gurantor2 in ctx.MCustomer on db.ContractGurantor2 equals _gurantor2.CustomerCode into a4
                              from gurantor2 in a4.DefaultIfEmpty()

                              select new CreditCollectionLetterList
                              {
                                  ContractId = db.ContractId,
                                  ContractNo = db.ContractNo,
                                  ContractDate = db.ContractDate.Value.ToString("dd/MM/yyyy"),
                                  ContractHire = $"{contrachHire.CustomerPrename}{contrachHire.CustomerName} {contrachHire.CustomerSurname}",
                                  ContractGurantor1 = $"{gurantor1.CustomerPrename}{gurantor1.CustomerName} {gurantor1.CustomerSurname}",
                                  ContractGurantor2 = $"{gurantor2.CustomerPrename}{gurantor2.CustomerName} {gurantor2.CustomerSurname}",
                                  StatusDesc = status.StatusDesc,
                                  CommunicateType = cl.CommunicateType,
                                  AddressType = cl.AddressType
                              }).ToListAsync();

            return Ok(list);
        }

        [HttpGet("GetCustomerContract")]
        public async Task<IActionResult> GetCustomerContract(int contractId)
        {
            var head = await ctx.CreditCollectionLetter.Where(x => x.ContractId == contractId).FirstOrDefaultAsync();
            var detail = await ctx.CreditCollectionLetterDetail.Where(x => x.ContractId == contractId).ToListAsync();
            var group = new CreditCollectionLetterGroupList
            {
                Head = head,
                Detail = detail
            };

            return Ok(group);
        }

        [HttpPost("PostCustomerContract")]
        public async Task<IActionResult> PostCustomerContract([FromBody] CreditCollectionLetterGroup group)
        {
            if (group.Head.ClId != 0)
            {
                ctx.CreditCollectionLetter.Update(group.Head);
            }
            else
            {
                ctx.CreditCollectionLetter.Add(group.Head);
            }

            if (group.Detail.CldId != 0)
            {
                ctx.CreditCollectionLetterDetail.Update(group.Detail);
            }
            else
            {
                var bookNo = iSysParamService.GenerateContractBookNo(group.Head.BranchId);
                group.Detail.CldBookNo = bookNo;
                group.Detail.CldReferNo = bookNo;
                ctx.CreditCollectionLetterDetail.Add(group.Detail);
            }
            await ctx.SaveChangesAsync();

            return CreatedAtAction("GetCustomerContract", new { id = group.Head.ClId }, group.Head);
        }
    }
}
