using Dapper;
using Reminy.Core.Postgres.Contracts;
using Reminy.Core.TestDataInitialization.Models;

namespace Reminy.Core.TestDataInitialization.Tables;

public sealed class TagsTable(IConnectionFactory connectionFactory) : BaseTable(connectionFactory)
{
    private const string TableName = "tags";

    public async Task Insert(Tag tag)
    {
        await using var connection = ConnectionFactory.Create();

        await connection.ExecuteAsync(@"
                INSERT INTO
                    tags
                (id, name)
                VALUES (@Id, @Name);",
            new
            {
                Id = tag.Id,
                Name = tag.Name,
            });
    }

    public async Task Truncate()
        => await Truncate(TableName);
}