using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MStatus
    {
        public int Id { get; set; }
        public string StatusCode { get; set; }
        public string StatusDesc { get; set; }
        public bool Status { get; set; }
    }
}
