using System;
namespace KKHondaBackend.Models
{
  public partial class CarHistory
  {
    public int CarId { get; set; }
    public string CarNo { get; set; }
    public int? BookingId { get; set; }
    public string ENo { get; set; }
    public string FNo { get; set; }
    public string TagNo { get; set; }
    public string Province { get; set; }
    public int? BranchId { get; set; }
    public DateTime? TagRegis { get; set; }
    public DateTime? TagExpire { get; set; }
    public string PrbNo { get; set; }
    public string PrbCompany { get; set; }
    public DateTime? PrbRegis { get; set; }
    public DateTime? PrbExpire { get; set; }
    public string CommitNo { get; set; }
    public DateTime? CommitExpire { get; set; }
    public string WarNo { get; set; }
    public string WarCompany { get; set; }
    public DateTime? WarRegis { get; set; }
    public DateTime? WarExpire { get; set; }
    public string OwnerCode { get; set; }
    public string VisitorCode { get; set; }
    public string TypeName { get; set; }
    public string BrandName { get; set; }
    public string ColorName { get; set; }
    public string ModelName { get; set; }
    public decimal? EngineSize { get; set; }
  }

  public partial class CarHistoryRes : CarHistory
  {
    public string OwnerName { get; set; }
    public string VisitorName { get; set; }
  }
}

