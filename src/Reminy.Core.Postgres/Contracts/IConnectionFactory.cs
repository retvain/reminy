using Npgsql;

namespace Reminy.Core.Postgres.Contracts;

public interface IConnectionFactory
{
    NpgsqlConnection Create();
}