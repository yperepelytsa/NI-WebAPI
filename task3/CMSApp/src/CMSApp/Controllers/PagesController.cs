using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMSApp.Data;
using CMSApp.Models;
using System.Net;

namespace CMSApp.Controllers
{
    public class PagesController : Controller
    {
        private IPageRepository pageRepository { get; set; }

        public PagesController(ApplicationDbContext context)
        {
            this.pageRepository = new PageRepository(context);
        }

        // GET: Pages
        public async Task<IActionResult> CustomPage(string url)
        {
          
            if (url == null)
            {
                return NotFound();
            }

            
            var page = pageRepository.GetAllPages().Where(p => p.UrlName == url).SingleOrDefault();
            if (pageRepository.GetAllNavLinks().Where(p => p.PageId == page.PageId).Count() > 0)
                ViewData["Links"] = pageRepository.GetAllNavLinks().Where(p => p.ParentLinkId == page.PageId).ToList();
            else
                ViewData["Links"] = null;
            page.Content = WebUtility.HtmlDecode(page.Content);
            if (page == null||page.PageId==0)
            {
                return NotFound();
            }

            return View(page);
        }
        public async Task<IActionResult> Index(string sortOrder,string searchTitle, string searchUrl,int? page,int? chosenId)
        {           
           
            ViewBag.ContentSortParm = sortOrder == "Content" ? "content_desc" : "Content";
            ViewBag.DescrSortParm = sortOrder == "Descr" ? "descr_desc" : "Descr";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.UrlSortParm = sortOrder == "Url" ? "url_desc" : "Url";
            var pages = from m in pageRepository.GetAllPages()
                        select m;
            switch (sortOrder)
            {
                case "content_desc":
                    pages = pages.OrderByDescending(s => s.Content);
                    break;
                case "Content":
                    pages = pages.OrderBy(s => s.Content);
                    break;
                case "descr_desc":
                    pages = pages.OrderByDescending(s => s.Description);
                    break;
                case "Descr":
                    pages = pages.OrderBy(s => s.Description);
                    break;
                case "title_desc":
                    pages = pages.OrderByDescending(s => s.Title);
                    break;
                case "Title":
                    pages = pages.OrderBy(s => s.Title);
                    break;
                case "url_desc":
                    pages = pages.OrderByDescending(s => s.UrlName);
                    break;
                case "Url":
                    pages = pages.OrderBy(s => s.UrlName);
                    break;

                default:
                    pages = pages.OrderBy(s => s.UrlName);
                    break;
            }
            string url = "";
            if (chosenId != null && chosenId != 0)
                url = pages.Where(c => c.PageId == chosenId).Select(c => c.UrlName).First();


            if (!String.IsNullOrEmpty(searchTitle))
            {
                pages = pages.Where(s => s.Title.Contains(searchTitle));
            }
            if (!String.IsNullOrEmpty(searchUrl))
            {
                pages = pages.Where(s => s.UrlName.Contains(searchUrl));
            }
            var pager = new Pager(pages.Count(), page);
            pager.sortOrder = sortOrder;
            pager.searchTitle = searchTitle;
            pager.searchUrl = searchUrl;
           
            var viewModel = new MyIndexViewModel
            {
                Items = pages.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager,
                chosenId=chosenId,
                chosenUrl = url
            };

            return View(viewModel);
        }

        // GET: Pages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = pageRepository.GetPageByID(id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET: Pages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageId,Content,Description,Title,UrlName")] Page page)
        {
            if (ModelState.IsValid)
            {
                pageRepository.InsertPage(page);
                pageRepository.Save();
                return RedirectToAction("Index");
            }
            return View(page);
        }

        public bool UrlAvailable(string urlname,string initialUrl)
        {
            if (urlname == initialUrl) return true;
            return pageRepository.GetAllPages().Where(c=>c.UrlName==urlname).Count()==0;
        }

        // GET: Pages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = pageRepository.GetPageByID(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // POST: Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PageId,Content,Description,Title,UrlName")] Page page)
        {
            if (id != page.PageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pageRepository.UpdatePage(page);
                    pageRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.PageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(page);
        }

        // GET: Pages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = pageRepository.GetPageByID(id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // POST: Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
 
            pageRepository.DeletePage(id);
            pageRepository.Save();
            return RedirectToAction("Index");
        }

      [HttpGet, ActionName("Choose")]
        public async Task<IActionResult> Choose(int id)
        {
            return RedirectToAction("Index",new { chosenId=id});
        }

        [HttpGet, ActionName("ChooseTwo")]
        public async Task<IActionResult> Choose(int firstId, int secondId)
        {
            pageRepository.InsertRelatedPages(firstId, secondId );
            pageRepository.Save();
            return RedirectToAction("Index", new { chosenId = firstId });
        }



        private bool PageExists(int id)
        {
            return pageRepository.checkUrl(id);
        }
       
    }
}
