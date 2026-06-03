using Storefront.API.Data;
using Storefront.API.Services;
using System.Reflection;

namespace Storefront.API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<UnitOfWork>();
        }

        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            //var types = Assembly.GetExecutingAssembly().GetTypes()
            //    .Where(t => t.IsClass && !t.IsAbstract && t.Namespace == "Storefront.API.Services");

            //foreach(var type in types)
            //{
            //    ServiceDescriptor descriptor = new ServiceDescriptor(type, Activator.CreateInstance);
            //}
            services.AddScoped<UserService>();
            services.AddScoped<RoleService>();
            return services;
        }
    }
}
