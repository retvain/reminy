using AutoFixture;
using Reminy.Core.IntegrationTests.Data;

namespace Reminy.Core.IntegrationTests.Tests;

public abstract class BaseTest
{
    protected IFixture Fixture { get; } = new Fixture();

    protected readonly DataInitializer DataInitializer = SetUpGlobal.GetService<DataInitializer>();
}