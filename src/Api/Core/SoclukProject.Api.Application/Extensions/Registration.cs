using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SoclukProject.Api.Application.Extensions;

public static class Registration
{
    public static IServiceCollection AddApplicationResgistration(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });

        //services.AddScoped(typeof(IRequestHandler<LoginUserCommand, LoginUserViewModel>), typeof(LoginUserCommandHandler));



        services.AddAutoMapper(assembly);
        services.AddValidatorsFromAssembly(assembly);



        return services;
    }
}

