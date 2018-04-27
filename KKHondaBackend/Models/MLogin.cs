using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MLogin
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string EmpSurname { get; set; }
        public string EmpEmail { get; set; }
        public string EmpIdNo { get; set; }
        public string BranchCode { get; set; }
        public string WorkgroupCode { get; set; }
        public string PositionCode { get; set; }
        public string Active { get; set; }
        public string Lock { get; set; }
        public string Phone { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
