using AutoFixture;
using NUnit.Framework;
using Reminy.Core.Host.Dto;

namespace Reminy.Core.IntegrationTests.Tests.User;

public sealed class UserTests
{
    private readonly Fixture _fixture = new();

    [Test]
    [Ignore("not ready")]
    public async Task RegisterValidUserTest()
    {
        var request = new RegisterUserRequestDto
        {
            Email = _fixture.Create<string>(),
            FirstName = _fixture.Create<string>(),
            LastName = _fixture.Create<string>(),
            Password = _fixture.Create<string>()
        };

        await SetUpGlobal.Client.RegisterUser(request);
    }
}