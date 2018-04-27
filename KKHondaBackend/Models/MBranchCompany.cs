using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MBranchCompany
    {
        public string BranchCompanyCode { get; set; }
        public string BranchCode { get; set; }
        public string BranchCompanyName { get; set; }

        public MBranch BranchCodeNavigation { get; set; }
    }
}
