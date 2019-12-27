using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
  public partial class Company
  {
    public int ComId { get; set; }
    public string ComCode { get; set; }
    public string ComName { get; set; }
    public string TypePersonal { get; set; }
    public string TaxId { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string AmphorCode { get; set; }
    public string ProvinceCode { get; set; }
    public string Zipcode { get; set; }
    public int? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public int? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
  }
}
