using Dapper;
using Reminy.Core.Postgres.Contracts;

namespace Reminy.Core.TestDataInitialization.Tables;

public abstract class BaseTable(IConnectionFactory connectionFactory)
{
    protected readonly IConnectionFactory ConnectionFactory = connectionFactory;

    protected async Task Truncate(string tableName)
    {
        await using var connection = ConnectionFactory.Create();
        await connection.ExecuteAsync($@"
DO $$
BEGIN
    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}') THEN
        TRUNCATE TABLE {tableName};
    END IF;
END $$");
    }
}