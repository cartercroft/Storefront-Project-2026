using AutoMapper;
using Storefront.API.Classes;
using Storefront.API.Data.Models;
using Storefront.API.Data.Repositories;
using Storefront.API.Models;
using Storefront.API.Services.Base;

namespace Storefront.API.Services
{
    public class ApplicationUserService : ServiceBase<ApplicationUserViewModel, ApplicationUser, Guid>
    {
        private readonly ApplicationUserRepository _applicationUserRepository;
        public ApplicationUserService(ApplicationUserRepository applicationUserRepository, IMapper mapper, Serilog.ILogger logger) : base(applicationUserRepository, mapper, logger)
        {
            _applicationUserRepository = applicationUserRepository;
        }
    }
}
