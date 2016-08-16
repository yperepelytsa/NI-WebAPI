using CMSApp.Models;
using System.Collections.Generic;

namespace CMSApp.Models
{
    public class MyIndexViewModel
    {
        public IEnumerable<Page> Items { get; set; }
        public Pager Pager { get; set; }
        public int? chosenId { get; set; }
        public string chosenUrl { get; set; }
    }
}
