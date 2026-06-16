using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storefront.API.Classes;
using Storefront.API.Models;
using Storefront.API.Services;

namespace Storefront.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationUserService _userService;
        public UsersController(ApplicationUserService userService) 
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<Response<IEnumerable<ApplicationUserModel>>> GetAll()
        {
            try
            {
                return await _userService.GetAll();
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ApplicationUserModel>> { ErrorMessages = { "An unknown error has occurred." } };
            }
        }
    }
}
