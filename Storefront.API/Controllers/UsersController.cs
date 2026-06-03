using Microsoft.AspNetCore.Mvc;
using Storefront.API.Classes;
using Storefront.API.Models;
using Storefront.API.Services;

namespace Storefront.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        public UsersController(UserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<Response<ApplicationUserModel>> Register(RegisterUserModel model)
        {
            try
            {
                if(!ModelState.IsValid) 
                {
                    return new Response<ApplicationUserModel>()
                    {
                        ErrorMessages = ModelState.Values.SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                    };
                }

                return await _userService.Register(model);
            }
            catch (Exception ex)
            {
                return new Response<ApplicationUserModel> { ErrorMessages = { "An unknown error has occurred." } };
            }
        }
    }
}
