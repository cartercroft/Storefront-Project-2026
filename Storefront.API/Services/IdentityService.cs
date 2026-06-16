using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Storefront.API.Classes;
using Storefront.API.Data.Models;
using Storefront.API.Models;

namespace Storefront.API.Services
{
    public class IdentityService : UserManager<ApplicationUser>
    {
        public IdentityService(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public async Task<Response<ApplicationUserModel>> Register(RegisterUserModel viewModel)
        {
            //TODO: Fix mapping once AutoMapper is implemented.
            ApplicationUser user = new ApplicationUser()
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                UserName = viewModel.UserName,
                CreateDate = DateTime.Now
            };

            IdentityResult createResult = await CreateAsync(user, viewModel.Password);

            if (!createResult.Succeeded)
            {
                return new Response<ApplicationUserModel>() { ErrorMessages = createResult.Errors.Select(e => $"Error Code: {e.Code} Description: {e.Description}").ToList() };
            }

            ApplicationUserModel result = new ApplicationUserModel
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email
            };

            return new Response<ApplicationUserModel>(result);
        }
    }
}
