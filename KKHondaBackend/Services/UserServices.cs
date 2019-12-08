using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using KKHondaBackend.Data;

namespace KKHondaBackend.Services
{
  public interface IUserServices
  {
    Dropdown[] GetDropdowns();
  }
  // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
  public class UserServices : IUserServices
  {
    private readonly dbwebContext ctx;

    public UserServices(dbwebContext context)
    {
      ctx = context;
    }

    public Dropdown[] GetDropdowns()
    {
      Dropdown[] userDropdowns = new Dropdown[] { };

      userDropdowns = (from db in ctx.User
                       where db.Enable == 1
                       select new Dropdown
                       {
                         Value = db.Id.ToString(),
                         Text = db.FullName
                       }).ToArray();

      return userDropdowns.ToArray();
    }

  }


}
