using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using Reminy.Core.Host.Dto;
using Reminy.Core.IntegrationTests.Data;

namespace Reminy.Core.IntegrationTests.Tests.Note;

public sealed class NoteTests
{
    private readonly Fixture _fixture = new();
    private readonly NoteInitializer _noteInitializer = SetUpGlobal.GetService<NoteInitializer>();

    [Test]
    public async Task CreateOneValidNote_Test()
    {
        var request = new CreateNoteRequestDto
        {
            Title = _fixture.Create<string>(),
            Content = _fixture.Create<string>()
        };

        var result = await SetUpGlobal.Client.CreateNote(request);

        result.Id.Should().NotBe(default);
        result.Id.Should().BeGreaterThan(0);
        result.Title.Should().Be(request.Title);
        result.Content.Should().Be(request.Content);
    }

    [Test]
    public async Task UpdateOneValidNote_Test()
    {
        var initialNote = await _noteInitializer.AddNote();

        var request = new UpdateNoteRequestDto
        {
            Id = initialNote.Id!.Value,
            Title = _fixture.Create<string>(),
            Content = _fixture.Create<string>()
        };

        var result = await SetUpGlobal.Client.UpdateNote(request);

        result.Id.Should().Be(request.Id);
        result.Title.Should().Be(request.Title).And.NotBe(initialNote.Title);
        result.Content.Should().Be(request.Content).And.NotBe(initialNote.Content);
    }

    [TearDown]
    public async Task TearDown()
        => await _noteInitializer.Clean();
}