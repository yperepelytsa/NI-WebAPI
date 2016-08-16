using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CMSApp.Models
{
    public class Page
    {
        public Page() { }
        [Key]
        public int PageId { get; set; }
        [Remote("UrlAvailable", "Pages", AdditionalFields = "InitialUrl")]
        public string UrlName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        [NotMapped]
        public string ShortContent
        {
            get
            {
                return this.Content.Substring(0, 15)+"...";
            }
        }
        [NotMapped]
        public bool IsSelected { get; set; }
        [InverseProperty("Page")]
        public List<NavLink> NavLinks { get; set; }
        [InverseProperty("ParentLink")]
        public List<NavLink> NavLinksParent { get; set; }
        public override String ToString()
        {
            return "Page Id: " + PageId + " UrlName: " + UrlName + " Title: " + Title + " Descr: " + Description + " Content: " + Content;
        }
    }
}
