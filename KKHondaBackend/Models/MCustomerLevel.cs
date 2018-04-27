using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MCustomerLevel
    {
        public MCustomerLevel()
        {
            MCustomer = new HashSet<MCustomer>();
        }

        public string LevelCode { get; set; }
        public string LevelName { get; set; }
        public string LevelDesc { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public ICollection<MCustomer> MCustomer { get; set; }
    }
}
