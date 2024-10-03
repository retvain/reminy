using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Reminy.Core.DomainServices.User.Commands.Register;
using Reminy.Core.DomainServices.User.Commands.Register.Contracts;

namespace Reminy.Core.DomainServices;

public static class DomainServicesRegistration
{
    public static void Configure(IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<RegisterUserCommand, Unit>, RegisterUserHandler>();
    }
}