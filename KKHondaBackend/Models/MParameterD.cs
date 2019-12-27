using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
  public partial class MParameterD
  {
    public int ParamDtId { get; set; }
    public int ParamHdId { get; set; }    
    public string Branch { get; set; }
    public string Year { get; set; }
    public string Month { get; set; }
    public int RunningNo { get; set; }

  }
}