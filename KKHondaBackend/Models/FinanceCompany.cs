

using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
  public partial class FinanceCompany
  {
    public int FicId { get; set; }
    public string FicCode { get; set; }
    public string FicName { get; set; }
    public string Address { get; set; }
    public string AmphorCode { get; set; }
    public string ProvinceCode { get; set; }
    public string TypePersonal { get; set; }
    public string Zipcode { get; set; }
    public string TaxId { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
  }
}
