using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMSApp.Models
{
    public class RelatedPages
    {
        public RelatedPages() { } 
        [Key]       
        public int RelatedPagesId { get; set; }
        public int Page1Id { get; set; }
        public Page Page1 { get; set; }
        public int Page2Id { get; set; }
        public Page Page2 { get; set; }
        public override string ToString()
        {
            return "RelatedPages Id: " + RelatedPagesId + " Page1Id: " + Page1Id + " Page2Id: " + Page2Id;
        }
    }

}
