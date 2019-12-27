using System.Threading.Tasks;
using KKHondaBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace KKHondaBackend.Controllers.Finances
{
  [ApiController]
  [Produces("application/json")]
  [Route("api/[controller]")]
  public class FinanceListController : Controller
  {
    private readonly dbwebContext ctx;

    public FinanceListController(
        dbwebContext _ctx
    )
    {
      ctx = _ctx;
    }
  }
}