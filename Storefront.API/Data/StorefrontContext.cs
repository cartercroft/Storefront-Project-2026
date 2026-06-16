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
    }
}
