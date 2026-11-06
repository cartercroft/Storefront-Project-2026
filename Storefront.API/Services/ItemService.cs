using AutoMapper;
using CroftMicroservices.Services;
using Storefront.API.Data;
using Storefront.API.Data.Models;
using Storefront.API.Data.Repositories;
using Storefront.API.Models;
using Storefront.Shared.Search;

namespace Storefront.API.Services
{
    public class ItemService : ServiceBase<ItemViewModel, Item, Guid, StorefrontContext>
    {
        public ItemService(ItemRepository repository, IMapper mapper, ILogger logger) : base(repository, mapper, logger)
        {
        }

        internal async Task<Response<IEnumerable<ItemViewModel>>> Search(ItemSearchParameters searchParams)
        {
            throw new NotImplementedException();
        }
    }
}
