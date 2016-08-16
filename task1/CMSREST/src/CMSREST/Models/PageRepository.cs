using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using CMSREST.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CMSREST.Models
{
    public class PageRepository : IPageRepository
    {
        private ApplicationDbContext context;

        public PageRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public void Add(Page item)
        {
            context.Pages.Add(item);
            
        }

        public IEnumerable<Page> GetAll()
        {
            return context.Pages.ToList();
        }

        public Page Remove(int id)
        {
            if (context.Pages.Where(m => m.PageId == id).Count() == 0) return null;
            var page = context.Pages.SingleOrDefault(m => m.PageId == id);
            context.Pages.Remove(page);
            context.SaveChanges();
            return page;
        }

        public void Update(Page item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
