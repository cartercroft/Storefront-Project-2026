using Microsoft.AspNetCore.Identity;

namespace Storefront.API.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}
