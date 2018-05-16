using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class Branch
    {
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public int? BranchCompanyId { get; set; }
        public string BranchName { get; set; }
        public string BranchRd { get; set; }
        public string BranchAddress { get; set; }
        public string BranchDistrict { get; set; }
        public string BranchProvince { get; set; }
        public int? BranchZipcode { get; set; }
        public string BranchLat { get; set; }
        public string BranchLng { get; set; }
        public string BranchContactName { get; set; }
        public string BranchContactNo { get; set; }
        public string BranchEmail { get; set; }
        public int? ZoneId { get; set; }
        public string BranchParentCode { get; set; }
        public string BranchRegisterNo { get; set; }
        public int BranchOrderFlag { get; set; }
        public string BranchDealerCode { get; set; }
        public int BranchEnable { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
