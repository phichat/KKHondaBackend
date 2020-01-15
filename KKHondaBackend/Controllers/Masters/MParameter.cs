using System.Collections.Generic;
using KKHondaBackend.Data;
using KKHondaBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KKHondaBackend.Controllers.Master
{
  [ApiController]
  [Produces("application/json")]
  [Route("api/Master/[controller]")]
  public class MParameterController : Controller
  {
    private readonly ISysParameterService sysParameter;
    public MParameterController(
      ISysParameterService _sysParameter
    )
    {
      sysParameter = _sysParameter;
    }

    [HttpGet("[Action]")]
    public string GetDepositNo(int branchId)
    {
      return sysParameter.GenerateDepositNo(branchId);
    }

    [HttpGet("[Action]")]
    public string GetReturnDepositNo(int branchId)
    {
      return sysParameter.GenerateReturnDepositNo(branchId);
    }

    [HttpGet("[Action]")]
    public string GetReceiptNo(int branchId)
    {
      return sysParameter.GenerateReceiptNo(branchId);
    }

  }
}