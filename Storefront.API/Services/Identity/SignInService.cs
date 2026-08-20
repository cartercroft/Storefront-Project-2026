using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Storefront.API.Classes;
using Storefront.API.Data.Models;
using Storefront.API.Data.Repositories;
using Storefront.API.Models;

namespace Storefront.API.Services.Identity
{
    public class SignInService : SignInManager<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationUserRepository _userRepository;
        public SignInService(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<ApplicationUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<ApplicationUser> confirmation, ApplicationUserRepository userRepository) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public async Task<Response> Login(LoginViewModel model)
        {

            Response response = new Response();

            ApplicationUser? user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                response.ErrorMessages.Add($"Unable to find user by email {model.Email}.");
                Logger.Log(LogLevel.Information, response.ErrorMessage);
                return response;
            }
            if (string.IsNullOrEmpty(user.UserName))
            {
                response.ErrorMessages.Add($"Username missing for email {model.Email}.");
                Logger.Log(LogLevel.Information, response.ErrorMessage);
                return response;
            }

            SignInResult result = await base.PasswordSignInAsync(user.UserName, model.Password, true, false);

            if (!result.Succeeded)
            {
                response.ErrorMessages.Add("Invalid username or password.");
                Logger.Log(LogLevel.Information, $"Failed login for user {user.Email}.");
            }
            else
            {
                user.LastLoginDate = DateTime.Now;
                await _userRepository.Save(user);
                Logger.Log(LogLevel.Information, $"Successful login for user {user.Email}.");
            }

            return response;
        }
    }
}
