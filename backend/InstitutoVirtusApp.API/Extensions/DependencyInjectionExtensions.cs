using InstitutoVirtusApp.Domain.Interfaces;
using InstitutoVirtusApp.Infrastructure.Security;

namespace InstitutoVirtusApp.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();

        return services;
    }
}
