namespace Storefront.API.Models
{
    public class ApplicationUserViewModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
