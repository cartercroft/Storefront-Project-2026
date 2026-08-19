using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storefront.API.Classes;
using Storefront.API.Data.Models;
using Storefront.API.Models;
using Storefront.API.Services;

namespace Storefront.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : CrudControllerBase<ApplicationRoleViewModel, ApplicationRole, Guid>
    {
        private readonly ApplicationRoleService _roleService;
        public RolesController(ApplicationRoleService service) : base(service)
        {
            _roleService = service;
        }
        [HttpGet]
        public async Task<Response<IEnumerable<ApplicationRoleViewModel>>> GetAll()
        {
            return await _roleService.GetAll();
        }
    }
}
