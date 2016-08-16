using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CMSApp.Data;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CMSApp.ViewComponents
{
    public class RelPagesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public RelPagesViewComponent (ApplicationDbContext context)
        {
            db = context;
        }
        public async Task<IViewComponentResult> InvokeAsync( int id )
        {
            var items = db.Pages.Where(x=>db.RelatedPages.Any(r=>r!=null&&((r.Page1Id==id&&r.Page2Id==x.PageId)||(r.Page2Id == id && r.Page1Id == x.PageId))));
            return View(items);
        }

    }
}
