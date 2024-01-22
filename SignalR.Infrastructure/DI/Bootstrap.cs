using SignalR.Infrastructure.Reposatory;
using Microsoft.Extensions.DependencyInjection;

namespace SignalR.Infrastructure.DI
{
    public static class Bootstrap
    {
        public static IServiceCollection AddInfrastructureStrapping(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
