using Reminy.Core.Postgres;
using Reminy.Core.Postgres.Contracts;

namespace Reminy.Core.Host.Composition.Postgres;

internal static class PostgresExtension
{
    public static IServiceCollection AddPostgres(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionFactory, ConnectionFactory>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            var postgresConfiguration = configuration
                .GetRequiredSection("Postgres")
                .GetRequired<PostgresConfiguration>();

            return new ConnectionFactory(postgresConfiguration);
        });

        services.AddPostgresServices();

        return services;
    }
}