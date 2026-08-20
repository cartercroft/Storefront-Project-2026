using CroftMicroservices.Data;
using Storefront.API.Data.Models;

namespace Storefront.API.Data.Repositories
{
    public class ItemRepository : RepositoryBase<Item, Guid, StorefrontContext>
    {
        public ItemRepository(StorefrontContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }
    }
}
