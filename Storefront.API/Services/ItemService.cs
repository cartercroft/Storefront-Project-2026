using AutoMapper;
using Storefront.API.Data.Models;
using Storefront.API.Data.Repositories;
using Storefront.API.Models;
using Storefront.API.Services.Base;

namespace Storefront.API.Services
{
    public class ItemService : ServiceBase<ItemViewModel, Item, Guid>
    {
        public ItemService(ItemRepository repository, IMapper mapper, Serilog.ILogger logger) : base(repository, mapper, logger)
        {
        }
    }
}
