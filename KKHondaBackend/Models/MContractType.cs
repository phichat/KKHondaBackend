using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MContractType
    {
        public int Id { get; set; }
        public string TypeCode { get; set; }
        public string TypeDesc { get; set; }
        public bool Status { get; set; }
    }
}
