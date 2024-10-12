using Microsoft.Extensions.DependencyInjection;
using Reminy.Core.DomainServices.Note.Ports;
using Reminy.Core.Postgres.Contracts;
using Reminy.Core.Postgres.Stores;

namespace Reminy.Core.Postgres;

public static class PostgresExtensions
{
    public static void AddPostgresServices(this IServiceCollection services)
    {
        services.AddSingleton<IMigrator, Migrator>();
        services.AddSingleton<INoteStore, NoteStore>();
    }
}