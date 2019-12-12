using System;
namespace KKHondaBackend.Models
{
  public partial class CarRegisSedList
  {
    public int SedId { get; set; }
    public string SedNo { get; set; }
    public string ConList { get; set; }
    public decimal Price1 { get; set; }
    public decimal VatPrice1 { get; set; }
    public decimal NetPrice1 { get; set; }
    public decimal Price2 { get; set; }
    public decimal? Price3 { get; set; }
    public decimal Price2Remain { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal BorrowMoney { get; set; }
    public int Status { get; set; }
    public int BranchId { get; set; }
    public int CreateBy { get; set; }
    public DateTime CreateDate { get; set; }
    public int? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string Reason { get; set; }
    public string Remark { get; set; }
  }

  public partial class CarRegisSedListRes : CarRegisSedList
  {
    public string StatusDesc { get; set; }
    public string BranchName { get; set; }
    public string CreateName { get; set; }
    public string UpdateName { get; set; }
  }

  public partial class CarRegisSedCancel
  {
    public string SedNo { get; set; }
    public string Reason { get; set; }
  }

  public class SearchSedList
  {
    public string SedNo { get; set; }
    public DateTime? CreateDate { get; set; }
    public string CreateName { get; set; }
    public int? Status { get; set; }
  }
}