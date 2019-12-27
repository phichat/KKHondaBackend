using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
  public partial class MParameter
  {
    public int ParamHdId { get; set; }
    public string Module { get; set; }
    public string ModuleDesc { get; set; }
    public string Prefix { get; set; }
    public string Subfix { get; set; }
    public int RunningNo { get; set; }
  }
}