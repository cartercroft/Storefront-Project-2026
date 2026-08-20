using Storefront.API.Data.Models;
using CroftMicroservices.Data;

namespace Storefront.API.Data.Repositories
{
    public class ApplicationRoleRepository : RepositoryBase<ApplicationRole, Guid, StorefrontContext>
    {
        public ApplicationRoleRepository(StorefrontContext dbContext, ILogger logger) : base(dbContext, logger){}
    }
}
