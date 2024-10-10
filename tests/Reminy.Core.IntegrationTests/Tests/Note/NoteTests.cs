using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using Reminy.Core.Host.Dto;

namespace Reminy.Core.IntegrationTests.Tests.Note;

public sealed class NoteTests : BaseTest
{
    [Test]
    public async Task CreateOneValidNote_Test()
    {
        var request = new CreateNoteRequestDto
        {
            Header = Fixture.Create<string>(),
            Content = Fixture.Create<string>()
        };

        var result = await SetUpGlobal.Client.CreateNote(request);

        result.Id.Should().NotBe(default);
        result.Id.Should().BeGreaterThan(0);
        result.Header.Should().Be(request.Header);
        result.Content.Should().Be(request.Content);
    }
}