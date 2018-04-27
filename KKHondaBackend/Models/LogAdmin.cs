using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class LogAdmin
    {
        public int LogId { get; set; }
        public int LogUserid { get; set; }
        public string LogBrowser { get; set; }
        public string LogIp { get; set; }
        public DateTime? LogDate { get; set; }
    }
}
