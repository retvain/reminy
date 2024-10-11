using Npgsql;
using Reminy.Core.Postgres.Contracts;

namespace Reminy.Core.TestDataInitialization;

public sealed class TestNoteStore(IConnectionFactory connectionFactory) : IDisposable
{
    public void Truncate()
    {
        using var connection = connectionFactory.Create();
        using var command = connection.CreateCommand();

        TruncateTable(command, "note_tags");
        TruncateTable(command, "notes");
        TruncateTable(command, "tags");
    }

    private static void TruncateTable(NpgsqlCommand command, string table)
    {
        command.CommandText = $"TRUNCATE {table}";
        command.ExecuteNonQuery();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}