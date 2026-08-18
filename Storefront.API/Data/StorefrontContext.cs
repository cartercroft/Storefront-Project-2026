using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Storefront.API.Data.Models;

namespace Storefront.API.Data
{
    public class StorefrontContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public StorefrontContext(){}
        public StorefrontContext(DbContextOptions<StorefrontContext> options) : base(options){}
        new public DbSet<ApplicationUser> Users { get; set; }
        new public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<Item> Items { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(StorefrontContext).Assembly);
        }
    }
}
