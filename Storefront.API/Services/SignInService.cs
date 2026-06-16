using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Storefront.API.Classes;
using Storefront.API.Data.Models;
using Storefront.API.Models;

namespace Storefront.API.Services
{
    public class SignInService : SignInManager<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public SignInService(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<ApplicationUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<ApplicationUser> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            _userManager = userManager;
        }
        public async Task<Response> Login(LoginModel model)
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

            throw new Exception("This is a huge error");
            SignInResult result = await base.PasswordSignInAsync(user.UserName, model.Password, true, false);

            if (!result.Succeeded)
            {
                response.ErrorMessages.Add("Invalid username or password.");
                Logger.Log(LogLevel.Information, $"Failed login for user {user.Email}.");
            }
            else
            {
                Logger.Log(LogLevel.Information, $"Successful login for user {user.Email}.");
            }

            return response;
        }
    }
}
