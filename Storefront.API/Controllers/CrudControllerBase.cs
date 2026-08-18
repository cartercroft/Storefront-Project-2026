using Microsoft.AspNetCore.Mvc;
using Storefront.API.Classes;
using Storefront.API.Services.Base;

namespace Storefront.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CrudControllerBase<TViewModel, TModel, TKey> : ControllerBase
        where TViewModel : class
        where TModel : class
        where TKey : struct, IEquatable<TKey>, IComparable<TKey>
    {
        private readonly ServiceBase<TViewModel, TModel, TKey> _service;
        public CrudControllerBase(ServiceBase<TViewModel, TModel, TKey> service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<Response<TViewModel?>> Get(TKey id)
        {
            return await _service.GetById(id);
        }
        [HttpPost]
        public async Task<Response<TViewModel>> Save(TViewModel viewModel)
        {
            return await _service.Save(viewModel);
        }
        [HttpDelete]
        public async Task<Response<bool>> Delete(TViewModel viewModel)
        {
            return _service.Delete(viewModel);
        }
    }
}
