using Npgsql;
using Reminy.Core.Postgres;
using Reminy.Core.Postgres.Contracts;

namespace Reminy.Core.Host.Composition.Postgres;

internal sealed class ConnectionFactory(PostgresConfiguration configuration) : IConnectionFactory
{
    public NpgsqlConnection Create()
    {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Pooling = false,
            Host = configuration.Host,
            Username = configuration.Username,
            Password = configuration.Password,
            Database = configuration.Database,
            Port = configuration.DefaultPort
        };

        var connection = OpenConnection(connectionStringBuilder.ConnectionString);

        return connection;
    }

    private NpgsqlConnection OpenConnection(string connectionString)
    {
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        return connection;
    }
}