using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CMSApp.Models;

namespace CMSApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private static bool created = false;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            if (!created)
            {
                this.Database.Migrate();
                //this.Add(new Movie { Title = "Title1", Genre = "Genre1", Price = 100, ReleaseDate = new DateTime(2010, 10, 20) });
               // this.Add(new Movie { Title = "Title2", Genre = "Genre2", Price = 101, ReleaseDate = new DateTime(2011, 11, 21) });
               // this.Add(new Movie { Title = "Title3", Genre = "Genre3", Price = 103, ReleaseDate = new DateTime(2013, 9, 19) });
                this.SaveChanges();
                created = true;
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

    public DbSet<Page> Pages { get; set; }
    public DbSet<NavLink> NavLinks { get; set; }
    public DbSet<RelatedPages> RelatedPages { get; set; }
    }
}
