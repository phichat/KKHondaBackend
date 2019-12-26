using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class Information
    {
        public int id { get; set; }
        public string code_type { get; set; }
        public int? code_id { get; set; }
        public string code_value { get; set; }        
    }
    
}