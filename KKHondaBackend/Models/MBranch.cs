using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MBranch
    {
        public MBranch()
        {
            MBranchCompany = new HashSet<MBranchCompany>();
        }

        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string BranchPhone { get; set; }
        public string BranchZone { get; set; }
        public string BranchParentCode { get; set; }
        public string BranchRegisterNo { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public ICollection<MBranchCompany> MBranchCompany { get; set; }
    }
}
