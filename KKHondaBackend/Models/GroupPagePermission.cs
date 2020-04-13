using System;

namespace KKHondaBackend.Models
{
    public partial class GroupPagePermission
    {
        public int GPPId { get; set; }
        public int? GId { get; set; }
        public int? PageId { get; set; }
        public int? Status { get; set; }
    }

    public partial class GroupPagePermissionRes
    {
        public int GPPId { get; set; }
        public int? GId { get; set; }
        public string GName { get; set; }
        public int? PageId { get; set; }
        public string PageName { get; set; }
    }
}