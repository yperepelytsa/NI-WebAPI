using CMSREST.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CMSREST.Models
{
    public class RelatedPagesRepository : IRelatedPagesRepository
    {
        private ApplicationDbContext context;

        public RelatedPagesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(RelatedPages item)
        {
            context.RelatedPages.Add(item);
        }

        public IEnumerable<RelatedPages> GetAll()
        {
            return context.RelatedPages.ToList();
        }

        public RelatedPages Remove(int id)
        {
            var RelatedPages = context.RelatedPages.SingleOrDefault(m => m.RelatedPagesId == id);
            context.RelatedPages.Remove(RelatedPages);
            context.SaveChanges();
            return RelatedPages;
        }

        public void Update(RelatedPages item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
