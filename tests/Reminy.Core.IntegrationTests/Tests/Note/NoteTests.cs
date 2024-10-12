using System.Net;
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
        var anotherNote = await _noteInitializer.AddNote();

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

        var readAnotherNote = await SetUpGlobal.Client.GetNote(new GetNoteRequestDto { Id = anotherNote.Id!.Value });

        readAnotherNote.Should().BeEquivalentTo(anotherNote);
    }

    [Test]
    public async Task ReadOneNote_Test()
    {
        var note = await _noteInitializer.AddNote();
        var request = new GetNoteRequestDto { Id = note.Id!.Value };

        var result = await SetUpGlobal.Client.GetNote(request);

        result.Id.Should().Be(note.Id);
        result.Title.Should().Be(note.Title);
        result.Content.Should().Be(note.Content);
    }

    [Test]
    public async Task ReadManyNotes_Test()
    {
        const int noteCount = 10;

        var notes = await _noteInitializer.AddNotes(noteCount);
        var request = new GetNotesRequestDto();

        var resultNotes = await SetUpGlobal.Client.GetNotes(request);

        resultNotes.Count.Should().Be(noteCount);

        foreach (var note in resultNotes)
        {
            var expected = notes.First(n => n.Id == note.Id);

            note.Title.Should().Be(expected.Title);
            note.Content.Should().Be(expected.Content);
        }
    }

    [Test]
    public async Task DeleteNote_Test()
    {
        var note = await _noteInitializer.AddNote();

        var requestBeforeDeleting = new GetNoteRequestDto { Id = note.Id!.Value };
        var resultBeforeDeleting = await SetUpGlobal.Client.GetNote(requestBeforeDeleting);

        resultBeforeDeleting.Id.Should().Be(note.Id);
        resultBeforeDeleting.Title.Should().Be(note.Title);
        resultBeforeDeleting.Content.Should().Be(note.Content);

        var request = new DeleteNoteRequestDto { Id = note.Id!.Value };
        await SetUpGlobal.Client.DeleteNote(request);

        var requestAfterDeleting = new GetNoteRequestDto { Id = note.Id!.Value };
        try
        {
            await SetUpGlobal.Client.GetNote(requestAfterDeleting);

            throw new AssertionException($"Note with id {note.Id} is not deleted");
        }
        catch (HttpRequestException e)
        {
            if (e.StatusCode != HttpStatusCode.NotFound)
                throw;
        }
    }

    [TearDown]
    public async Task TearDown()
        => await _noteInitializer.Clean();
}