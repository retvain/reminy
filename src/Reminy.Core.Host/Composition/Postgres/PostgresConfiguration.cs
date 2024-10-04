namespace Reminy.Core.Host.Composition.Postgres;

internal sealed class PostgresConfiguration
{
    public string? Host { get; set; }

    public int DefaultPort { get; init; }

    public string Database { get; set; } = string.Empty;

    public string? Username { get; set; }

    public string? Password { get; init; }
}