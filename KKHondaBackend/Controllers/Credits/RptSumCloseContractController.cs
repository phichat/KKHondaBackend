using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KKHondaBackend.Data;
using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KKHondaBackend.Controllers.Credits
{
    [Produces("application/json")]
    [Route("api/Credit/RptSumCloseContract")]
    public class RptSumCloseContractController : Controller
    {
        private readonly dbwebContext ctx;

        public RptSumCloseContractController(dbwebContext context)
        {
            ctx = context;
        }


        [HttpGet]
        public IActionResult Oninit()
        {
            try
            {
                var branch = ctx.Branch
                .Where(item => item.BranchEnable == 1)
                .Select(item => new
                {
                    BranchId = item.BranchId,
                    BranchName = item.BranchName
                }).ToList();

                var obj = new Dictionary<string, object>
                {
                    { "branch", branch }
                };

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        // GET: api/GetReportEndContract
        [HttpGet("GetByCon")]
        public IEnumerable<SpRptCloseContract> GetReportEndContract(string branchId, DateTime endContractDateStart, DateTime endContractDateEnd)
        {
            var sdate = endContractDateStart.Date;
            var edate = endContractDateEnd.Date;
            var obj = ctx.SpRptCloseContract
                .FromSql($"EXEC sp_RptCloseContract {branchId}, {sdate.ToString("yyyy-MM-dd")}, {edate.ToString("yyyy-MM-dd")}")
                .ToList();
            return obj;
        }
    }
}