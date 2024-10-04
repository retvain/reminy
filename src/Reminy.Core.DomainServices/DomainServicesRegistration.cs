using Microsoft.Extensions.DependencyInjection;

namespace Reminy.Core.DomainServices;

public static class DomainServicesRegistration
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        return services;
    }
}