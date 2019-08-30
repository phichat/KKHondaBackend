using System;
namespace KKHondaBackend.Models
{
    public class CarRegisMSendback
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public bool Checked { get; set; }
        public bool NewCar { get; set; }
        public bool Tag { get; set; }
        public bool Act { get; set; }
        public bool Warranty { get; set; }
        public bool CheckCar { get; set; }
        public bool Other { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }

        public class CarRegisMSendbackRes
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public bool Checked { get; set; }
        public bool NewCar { get; set; }
        public bool Tag { get; set; }
        public bool Act { get; set; }
        public bool Warranty { get; set; }
        public bool CheckCar { get; set; }
        public bool Other { get; set; }
        public int UpdateBy { get; set; }
        public string UpdateName { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreateBy { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}