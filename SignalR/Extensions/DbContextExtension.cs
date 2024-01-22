using Microsoft.EntityFrameworkCore;
using SignalR.Infrastructure.Context;

namespace SignalR.Extensions
{
    public static class DbContextExtension
    {
        public static IServiceCollection AddDataBaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: options =>
                {
                    options.EnableRetryOnFailure();
                    options.CommandTimeout(10);
                }));

            return services;
        }
    }
}
