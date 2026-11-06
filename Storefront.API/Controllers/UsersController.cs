using CroftMicroservices.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storefront.API.Data;
using Storefront.API.Data.Models;
using Storefront.API.Models;
using Storefront.API.Services;

namespace Storefront.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class UsersController : CrudControllerBase<ApplicationUserViewModel, ApplicationUser, Guid, StorefrontContext>
    {
        private readonly ApplicationUserService _userService;
        public UsersController(ApplicationUserService userService) : base(userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<Response<IEnumerable<ApplicationUserViewModel>>> GetAll()
        {
            return await _userService.GetAll();
        }
    }
}
