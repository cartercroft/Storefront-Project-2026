using Microsoft.AspNetCore.Mvc;
using Storefront.API.Classes;
using Storefront.API.Models;
using Storefront.API.Services;

namespace Storefront.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IdentityService _identityService;
        private readonly SignInService _signInService;
        public AuthController(IdentityService identityService, SignInService signInService)
        {
            _identityService = identityService;
            _signInService = signInService;
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

                return await _identityService.Register(model);
            }
            catch (Exception ex)
            {
                return new Response<ApplicationUserModel> { ErrorMessages = { "An unknown error has occurred." } };
            }
        }
        [HttpPost]
        public async Task<Response> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return new Response()
                {
                    ErrorMessages = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList()
                };
            }

            return await _signInService.Login(model);
        }
        [HttpPost]
        public async Task SignOut()
        {
            await _signInService.SignOutAsync();
        }
    }
}
