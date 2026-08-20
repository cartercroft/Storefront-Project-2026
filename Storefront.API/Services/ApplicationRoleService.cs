using AutoMapper;
using Storefront.API.Data;
using Storefront.API.Data.Base;
using Storefront.API.Data.Models;
using Storefront.API.Data.Repositories;
using Storefront.API.Models;
using Storefront.API.Services.Base;

namespace Storefront.API.Services
{
    public class ApplicationRoleService : ServiceBase<ApplicationRoleViewModel, ApplicationRole, Guid>
    {
        public ApplicationRoleService(ApplicationRoleRepository repository, IMapper mapper, Serilog.ILogger logger) : base(repository, mapper, logger)
        {
        }
    }
}
