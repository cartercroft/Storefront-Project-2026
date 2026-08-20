using AutoMapper;
using Storefront.API.Data.Models;
using Storefront.API.Data.Repositories;
using Storefront.API.Models;
using CroftMicroservices.Services;
using Storefront.API.Data;

namespace Storefront.API.Services
{
    public class ApplicationUserService : ServiceBase<ApplicationUserViewModel, ApplicationUser, Guid, StorefrontContext>
    {
        private readonly ApplicationUserRepository _applicationUserRepository;
        public ApplicationUserService(ApplicationUserRepository applicationUserRepository, IMapper mapper, ILogger logger) : base(applicationUserRepository, mapper, logger)
        {
            _applicationUserRepository = applicationUserRepository;
        }
    }
}
