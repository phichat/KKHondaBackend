using System;
using System.Collections.Generic;

namespace KKHondaBackend.Models
{
    public partial class MRelation
    {
        public int Id { get; set; }
        public string RelationCode { get; set; }
        public string RelationDesc { get; set; }
        public bool Status { get; set; }
    }
}
