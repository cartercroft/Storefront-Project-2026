using Microsoft.AspNetCore.Mvc;
using Storefront.API.Classes;
using Storefront.API.Data.Models;
using Storefront.API.Models;
using Storefront.API.Services;

namespace Storefront.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ItemController : CrudControllerBase<ItemViewModel, Item, Guid>
    {
        private readonly ItemService _service;
        public ItemController(ItemService service) : base(service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<Response<IEnumerable<ItemViewModel>>> GetAll()
        {
            return await _service.GetAll();
        }
    }
}
