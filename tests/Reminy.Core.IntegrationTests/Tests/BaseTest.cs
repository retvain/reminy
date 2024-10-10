using AutoFixture;

namespace Reminy.Core.IntegrationTests.Tests;

public abstract class BaseTest
{
    protected IFixture Fixture { get; } = new Fixture();
}