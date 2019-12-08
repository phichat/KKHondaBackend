using System;
using System.Collections.Generic;
using System.Linq;
using KKHondaBackend.Data;
using KKHondaBackend.Models;

namespace KKHondaBackend.Services
{
  public interface IRelationService
  {
    Dropdown[] GetDropdowns();
  }
  public class RelationService : IRelationService
  {
    private readonly dbwebContext ctx;

    public RelationService(dbwebContext context)
    {
      ctx = context;
    }

    public Dropdown[] GetDropdowns()
    {
      List<Dropdown> relations = new List<Dropdown>();

      relations = (from db in ctx.MRelation
                   where db.Status == true
                   select new Dropdown
                   {
                     Value = db.Id.ToString(),
                     Text = db.RelationDesc
                   }).ToList();

      return relations.ToArray();
    }
  }
}
