using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storefront.API.Classes;
using Storefront.API.Models;
using Storefront.API.Services.Identity;

namespace Storefront.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IdentityService _identityService;
        private readonly SignInService _signInService;
        private readonly IMapper _mapper;
        public AuthController(IdentityService identityService, SignInService signInService, IMapper mapper)
        {
            _identityService = identityService;
            _signInService = signInService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<Response<ApplicationUserViewModel>> Register(RegisterUserViewModel model)
        {
            if(!ModelState.IsValid) 
            {
                return new Response<ApplicationUserViewModel>()
                {
                    ErrorMessages = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList()
                };
            }

            return await _identityService.Register(model);
        }
        [HttpPost]
        public async Task<Response> Login(LoginViewModel model)
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
        public async Task Logout()
        {
            await _signInService.SignOutAsync();
        }
    }
}
