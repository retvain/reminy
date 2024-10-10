using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Reminy.Core.IntegrationTests.Tools;
using Reminy.Core.Postgres.Contracts;

namespace Reminy.Core.IntegrationTests;

[SetUpFixture]
internal sealed class SetUpGlobal
{
    private static TestWebApplicationFactory _factory = null!;
    public static TestClient Client = null!;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        _factory = new TestWebApplicationFactory();
        Client = new TestClient(_factory.CreateDefaultClient());

        GetService<IMigrator>().Migrate();
    }

    public static T GetService<T>() where T : notnull
        => _factory.Services.GetRequiredService<T>();
}