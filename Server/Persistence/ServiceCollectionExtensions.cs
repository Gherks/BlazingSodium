using BlazingSodium.Server.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingSodium.Server.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddTransient<EmployeeRepositoryInterface, EmployeeRepository>();
            return services;
        }
    }
}
