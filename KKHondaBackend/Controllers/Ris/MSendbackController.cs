using KKHondaBackend.Data;
using KKHondaBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using KKHondaBackend.Entities;
using System;
using KKHondaBackend.Services.Ris;

namespace KKHondaBackend.Controllers.Ris
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/Ris/[controller]")]
    public class MSendBackController : Controller
    {
        private readonly dbwebContext ctx;
        private readonly IMSendbackService iMSendback;

        public MSendBackController(
            dbwebContext _ctx,
            IMSendbackService _iMSendback
            )
        {
            ctx = _ctx;
            iMSendback =_iMSendback;
        }

        

    }
}