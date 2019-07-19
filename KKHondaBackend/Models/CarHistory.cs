using System;
namespace KKHondaBackend.Models
{
    public partial class CarHistory
    {
        public int CarId { get; set; }
        public string CarNo { get; set; }
        public int? BookingId { get; set; }
        public string ENo { get; set; }
        public string FNo { get; set; }
        public string TagNo { get; set; }
        public int? BranchId { get; set; }
        public string TagRegis { get; set; }
        public string TagExpire { get; set; }
        public string PrbNo { get; set; }
        public string PrbCompany { get; set; }
        public string PrbRegis { get; set; }
        public string PrbExpire { get; set; }
        public string CommitNo { get; set; }
        public string CommitExpire { get; set; }
        public string WarNo { get; set; }
        public string WarCompany { get; set; }
        public string WarRegis { get; set; }
        public string WarExpire { get; set; }
    }
}

