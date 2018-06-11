using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string TitleName { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int? UserType { get; set; }
        public int BranchId { get; set; }
        public int Enable { get; set; }
        public string Department { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? GId { get; set; }
    }
}
