using Microsoft.AspNetCore.Mvc;
using Storefront.API.Data.Models;
using Storefront.API.Models;
using Storefront.API.Services;
using CroftMicroservices.Controllers;
using Storefront.API.Data;
using Storefront.Shared.Search;

namespace Storefront.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ItemController : CrudControllerBase<ItemViewModel, Item, Guid, StorefrontContext>
    {
        private readonly ItemService _service;
        public ItemController(ItemService service) : base(service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<Response<IEnumerable<ItemViewModel>>> Search([FromQuery] ItemSearchParameters searchParams)
        {
            return await _service.Search(searchParams);
        }
    }
}
