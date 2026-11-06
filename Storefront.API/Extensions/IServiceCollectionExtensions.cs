using System.Reflection;

namespace Storefront.API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            List<string> namespacesToScan = new List<string>()
            {
                "Storefront.API.Services",
                "Storefront.API.Data.Repositories",
                "Storefront.API.Services.Identity"
            };

            var types = Assembly.GetAssembly(typeof(Program))!.DefinedTypes
                .Where(t => t.IsClass
                && !t.IsAbstract
                && !string.IsNullOrEmpty(t.Namespace)
                && namespacesToScan.Contains(t.Namespace))
                .ToList();

            List<Type> includedInterfaces = new List<Type>()
            {
                typeof(ILogger)
            };

            foreach (var type in types) { 
                IEnumerable<Type> interfacesToAdd = type.ImplementedInterfaces.Intersect(includedInterfaces);

                if (interfacesToAdd.Any())
                {
                    foreach (var interf in interfacesToAdd)
                    {
                        services.AddScoped(interf, type);
                    }
                }
                else
                {
                    services.AddScoped(type);
                }
            }
            //services.AddScoped<IdentityService>();
            //services.AddScoped<RoleService>();
            //services.AddScoped<SignInService>();
            //services.AddScoped<ApplicationUserService>();
            //services.AddScoped<ApplicationUserRepository>();
            //services.AddScoped<ApplicationRoleService>();
            //services.AddScoped<ApplicationRoleRepository>();
            //services.AddScoped<ItemService>();
            //services.AddScoped<ItemRepository>();
            return services;
        }
    }
}
