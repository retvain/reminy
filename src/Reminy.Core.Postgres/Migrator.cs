using Reminy.Core.Postgres.Contracts;

namespace Reminy.Core.Postgres;

internal sealed class Migrator(IConnectionFactory connectionFactory) : IMigrator
{
    public void Migrate()
    {
        var assembly = typeof(Migrator).Assembly;
        var scripts = assembly.GetManifestResourceNames()
            .Where(name => name.Contains($"Migrations") && name.EndsWith(".sql"));

        var connection = connectionFactory.Create();

        foreach (var scriptName in scripts.OrderBy(Path.GetFileName))
        {
            using var stream = assembly.GetManifestResourceStream(scriptName);
            using var reader = new StreamReader(stream!);

            var script = reader.ReadToEnd();

            var command = connection.CreateCommand();
            command.CommandText = script;
            command.ExecuteNonQuery();
        }
    }
}