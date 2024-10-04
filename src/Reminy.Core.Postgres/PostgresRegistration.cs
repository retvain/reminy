using Microsoft.Extensions.DependencyInjection;
using Reminy.Core.Postgres.Contracts;

namespace Reminy.Core.Postgres;

public static class PostgresExtensions
{
    public static void AddPostgresServices(this IServiceCollection services)
    {
        services.AddSingleton<IMigrator, Migrator>();
    }
}