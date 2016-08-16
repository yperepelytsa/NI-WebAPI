using CMSApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMSApp.Models
{
    public class PageRepository:IPageRepository,IDisposable
    {
        private ApplicationDbContext context;

        public PageRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Page> GetAllPages()
        {
            return context.Pages.ToList();
        }

        public Page GetPageByID(int? id)
        {
            return context.Pages.SingleOrDefault(m => m.PageId == id);
        }

        public void InsertPage(Page Page)
        {
            context.Pages.Add(Page);
        }

        public void DeletePage(int PageID)
        {
            var page = context.Pages.SingleOrDefault(m => m.PageId == PageID);
            context.Pages.Remove(page);
        }

        public void UpdatePage(Page Page)
        {
            context.Entry(Page).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void InsertRelatedPages(int page1Id, int page2Id)
        {
            context.RelatedPages.Add(new RelatedPages { Page1Id = page1Id, Page2Id = page2Id });
        }

        public bool checkUrl(int PageId)
        {
            return context.Pages.Any(e => e.PageId == PageId);
        }

        public IEnumerable<NavLink> GetAllNavLinks()
        {
            return context.NavLinks.ToList();
        }
    }
}
