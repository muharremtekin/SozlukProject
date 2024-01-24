using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoclukProject.Infrastructure.Persistance.Context;

namespace SoclukProject.Infrastructure.Persistance.Extensions;
public static class Registration
{
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SozlukContext>(options =>
        {
            var connectionString = configuration["connectionString"].ToString();
            options.UseNpgsql(connectionString);
        });
        return services;
    }
}

