using Storefront.API.Data.Base;
using Storefront.API.Data.Models;

namespace Storefront.API.Data.Repositories
{
    public class ApplicationRoleRepository : RepositoryBase<ApplicationRole, Guid>
    {
        public ApplicationRoleRepository(StorefrontContext dbContext) : base(dbContext){}
    }
}
