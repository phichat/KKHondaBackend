using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MContractGroup
    {
        public int Id { get; set; }
        public string GroupCode { get; set; }
        public string GroupDesc { get; set; }
        public bool Status { get; set; }
    }
}
