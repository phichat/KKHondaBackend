using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KKHondaBackend.Data;
using KKHondaBackend.Models;



namespace KKHondaBackend.Services.Ris
{
    public interface IMSendbackService
    {
        IEnumerable<CarRegisMSendback> Active { get; }
    }

    public class MSendbackService : IMSendbackService
    {
        private readonly dbwebContext ctx;

        public MSendbackService(dbwebContext context)
        {
            ctx = context;
        }

        public IEnumerable<CarRegisMSendback> Active
        {
            get => ctx.CarRegisMSendback.Where(x => x.Status == true).AsNoTracking();
        }
    }
}