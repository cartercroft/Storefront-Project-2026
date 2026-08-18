using Storefront.API.Data.Models;
using Storefront.API.Models;
using Storefront.API.Services;

namespace Storefront.API.Controllers
{
    public class RolesController : CrudControllerBase<ApplicationRoleViewModel, ApplicationRole, Guid>
    {
        public RolesController(ApplicationRoleService service) : base(service)
        {
        }
    }
}
