using Microsoft.AspNetCore.Identity;
using Storefront.API.Classes;
using Storefront.API.Data.Models;

namespace Storefront.API.Services.Identity
{
    public class RoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RoleService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Response<ApplicationRole>> DoSomething()
        {
            return new();
        }
    }
}
