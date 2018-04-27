using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MLocation
    {
        public string BranchCode { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string LocationDesc { get; set; }
        public string LocationZone { get; set; }
        public decimal? Width { get; set; }
        public decimal? Length { get; set; }
        public decimal? Height { get; set; }
        public decimal? Cbm { get; set; }
        public decimal? MaxPercentCbm { get; set; }
        public decimal? MaxWeight { get; set; }
        public decimal? MaxPercentWeight { get; set; }
        public string FixItemCode { get; set; }
        public decimal? MinimumUnit { get; set; }
        public decimal? MaximumUnit { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
