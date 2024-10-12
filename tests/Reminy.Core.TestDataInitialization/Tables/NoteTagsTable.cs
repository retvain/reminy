using Dapper;
using Reminy.Core.Postgres.Contracts;
using Reminy.Core.TestDataInitialization.Models;

namespace Reminy.Core.TestDataInitialization.Tables;

public sealed class NoteTagsTable(IConnectionFactory connectionFactory) : BaseTable(connectionFactory)
{
    private const string TableName = "note_tags";

    public async Task Insert(NoteTag noteTag)
    {
        await using var connection = ConnectionFactory.Create();

        await connection.ExecuteAsync(@"
                INSERT INTO
                    note_tags
                (note_id, tag_id)
                VALUES (@NoteId, @TagId);",
            new
            {
                NoteId = noteTag.NoteId,
                TagId = noteTag.TagId
            });
    }

    public async Task Truncate()
        => await Truncate(TableName);
}