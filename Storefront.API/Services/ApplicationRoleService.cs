using AutoMapper;
using Storefront.API.Data.Models;
using Storefront.API.Data.Repositories;
using Storefront.API.Models;
using Storefront.API.Data;
using CroftMicroservices.Services;

namespace Storefront.API.Services
{
    public class ApplicationRoleService : ServiceBase<ApplicationRoleViewModel, ApplicationRole, Guid, StorefrontContext>
    {
        public ApplicationRoleService(ApplicationRoleRepository repository, IMapper mapper, ILogger logger) : base(repository, mapper, logger)
        {
        }
    }
}
