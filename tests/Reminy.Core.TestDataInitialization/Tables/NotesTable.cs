using Dapper;
using Reminy.Core.Postgres.Contracts;
using Reminy.Core.TestDataInitialization.Models;

namespace Reminy.Core.TestDataInitialization.Tables;

public sealed class NotesTable(IConnectionFactory connectionFactory) : BaseTable(connectionFactory)
{
    private const string TableName = "notes";

    public async Task<Note> Insert(Note note)
    {
        await using var connection = ConnectionFactory.Create();

        var noteId = await connection.QuerySingleAsync<long>(@"
                INSERT INTO
                    notes
                (title, content)
                VALUES (@Title, @Content)
                RETURNING id;",
            new
            {
                Title = note.Title,
                Content = note.Content
            });

        return note with { Id = noteId };
    }

    public async Task Truncate()
        => await Truncate(TableName);
}