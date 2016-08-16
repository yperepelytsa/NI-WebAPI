using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSApp.Models
{
    public class NavLink
    {
        public NavLink() { }
        public int NavLinkId { get; set; }
        public string Title { get; set; }

        public int PageId { get; set; }
        [ForeignKey("PageId")]
        public Page Page { get; set; }

        public int ParentLinkId { get; set; }
        [ForeignKey("ParentLinkId")]
        public Page ParentLink { get; set; }

        public string Position { get; set; }
        public override String ToString()
        {
            return "Link Id: " + NavLinkId + " Title: " + Title + " PageId: " + PageId + " ParentLinkId: " + ParentLinkId;
        }
    }
}
