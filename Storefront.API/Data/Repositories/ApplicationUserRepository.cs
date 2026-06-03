using Storefront.API.Data.Base;
using Storefront.API.Data.Models;

namespace Storefront.API.Data.Repositories
{
    public class ApplicationUserRepository : RepositoryBase<ApplicationUser, Guid>
    {
        public ApplicationUserRepository(StorefrontContext dbContext) : base(dbContext){}
    }
}
