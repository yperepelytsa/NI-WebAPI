
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CMSREST.Models;

namespace CMSREST.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private static bool created = false;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            if (!created)
            {
                this.Database.Migrate();                
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

