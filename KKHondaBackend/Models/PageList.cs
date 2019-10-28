using System;

namespace KKHondaBackend.Models
{
    public partial class PageList
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public int? PageType { get; set; }
        public int? PagePos { get; set; }
        public int? PageMaster { get; set; }
    }
}