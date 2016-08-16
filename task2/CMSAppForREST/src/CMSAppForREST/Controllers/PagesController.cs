using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CMSApp.Models;

namespace CMSApp.Controllers
{
    public class PagesController : Controller
    {
        private IPageRepository pageRepository { get; set; }

        public PagesController()
        {
            pageRepository = new PageRepository();
        }

        // GET: Pages

        public async Task<IActionResult> Index(string searchTitle,int? page)
        {

            var pages = await pageRepository.GetAllPages(searchTitle);

            var pager = new Pager(pages.Count(), page);
            pager.searchTitle = searchTitle;

            var viewModel = new MyIndexViewModel
            {
                   Items = pages.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager        
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

            var page = await pageRepository.GetPageByID(id.Value);
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

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageId,Content,Description,Title,UrlName")] Page page)
        {
            if (ModelState.IsValid)
            {
                await pageRepository.InsertPage(page);
                return RedirectToAction("Index");
            }
            return View(page);
        }


        // GET: Pages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page =await  pageRepository.GetPageByID(id.Value);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

      
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
                    await pageRepository.UpdatePage(page);
                }
                catch (Exception)
                {

                        return NotFound();

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

            var page = await  pageRepository.GetPageByID(id.Value);
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
            await pageRepository.DeletePage(id);
            return RedirectToAction("Index");
        }



    }
}
