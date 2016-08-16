using CMSREST.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSREST.Models
{
    public class NavLinkRepository : INavLinkRepository
    {
        private ApplicationDbContext context;

        public NavLinkRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(NavLink item)
        {
            context.NavLinks.Add(item);
        }

        public IEnumerable<NavLink> GetAll()
        {
            return context.NavLinks.ToList();
        }

        public NavLink Remove(int id)
        {
            var NavLink = context.NavLinks.SingleOrDefault(m => m.NavLinkId == id);
            context.NavLinks.Remove(NavLink);
            context.SaveChanges();
            return NavLink;
        }

        public void Update(NavLink item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}