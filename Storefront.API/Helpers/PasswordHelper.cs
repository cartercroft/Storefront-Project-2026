namespace Storefront.API.Helpers
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, workFactor: 13);
        }
        public static bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, storedHash);
        }
    }
}
