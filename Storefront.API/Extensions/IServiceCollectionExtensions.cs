using Storefront.API.Data;
using Storefront.API.Data.Repositories;
using Storefront.API.Services;
using Storefront.API.Services.Identity;
using System.Reflection;

namespace Storefront.API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            //TODO: Assembly Scan
            //var types = Assembly.GetExecutingAssembly().GetTypes()
            //    .Where(t => t.IsClass && !t.IsAbstract && t.Namespace == "Storefront.API.Services");

            //foreach(var type in types)
            //{
            //    ServiceDescriptor descriptor = new ServiceDescriptor(type, Activator.CreateInstance);
            //}
            services.AddScoped<IdentityService>();
            services.AddScoped<RoleService>();
            services.AddScoped<SignInService>();
            services.AddScoped<ApplicationUserService>();
            services.AddScoped<ApplicationUserRepository>();
            services.AddScoped<ApplicationRoleService>();
            services.AddScoped<ApplicationRoleRepository>();
            services.AddScoped<ItemService>();
            services.AddScoped<ItemRepository>();
            return services;
        }
    }
}
