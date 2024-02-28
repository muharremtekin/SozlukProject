using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Infrastructure.Persistance.Context;
using SoclukProject.Infrastructure.Persistance.Repositories;


namespace SoclukProject.Infrastructure.Persistance.Extensions;
public static class Registration
{
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SozlukContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("sqlConnection");
            options.UseNpgsql(connectionString);
        });

        //var seedData = new SeedData();
        //seedData.SeedAsync(configuration).GetAwaiter().GetResult();

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
        services.AddScoped<IEntryRepository, EntryRepository>();
        services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();

        return services;
    }
}

