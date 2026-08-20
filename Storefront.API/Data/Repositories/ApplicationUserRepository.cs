using CroftMicroservices.Data;
using Storefront.API.Data.Models;

namespace Storefront.API.Data.Repositories
{
    public class ApplicationUserRepository : RepositoryBase<ApplicationUser, Guid, StorefrontContext>
    {
        public ApplicationUserRepository(StorefrontContext dbContext, ILogger logger) : base(dbContext, logger){}
    }
}
