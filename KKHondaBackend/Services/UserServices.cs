using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using KKHondaBackend.Data;

namespace KKHondaBackend.Services
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UserServices : IUserServices
    {
        private readonly dbwebContext ctx;

        public UserServices(dbwebContext context)
        {
            ctx = context;
        }

        public UserDropdown[] GetAllUserDropdowns()
        {
            UserDropdown[] userDropdowns = new UserDropdown[] { };

            userDropdowns = (from db in ctx.User
                        where db.Enable == 1
                        select new UserDropdown
                        {
                            Id = db.Id,
                            FullName = db.Fullname
                        }).ToArray();

            return userDropdowns;
        }

    }


}
