using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class CampaignH
    {
        public string Id { get; set; }
        public string BranchCode { get; set; }
        public string CampaignCode { get; set; }
        public string CampaignName { get; set; }
        public string CampaignDesc { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Active { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpateDate { get; set; }
    }
}
