using Storefront.API.Data.Base;
using Storefront.API.Data.Models;

namespace Storefront.API.Data.Repositories
{
    public class ItemRepository : RepositoryBase<Item, Guid, StorefrontContext>
    {
        public ItemRepository(StorefrontContext dbContext, Serilog.ILogger logger) : base(dbContext, logger)
        {
        }
    }
}
