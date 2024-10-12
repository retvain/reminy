using AutoFixture;
using Reminy.Core.TestDataInitialization.Models;
using Reminy.Core.TestDataInitialization.Tables;

namespace Reminy.Core.IntegrationTests.Data;

public sealed class NoteInitializer(NotesTable notesTable)
{
    private readonly Fixture _fixture = new();

    public async Task<Note> AddNote()
    {
        var note = new Note
        (
            Title: _fixture.Create<string>(),
            Content: _fixture.Create<string>()
        );

        return await notesTable.Insert(note);
    }

    public async Task<IReadOnlyCollection<Note>> AddNotes(int count)
    {
        var notes = new List<Note>();

        for (var i = 0; i < count; i++)
        {
            notes.Add(await AddNote());
        }

        return notes.ToArray();
    }

    public async Task Clean()
        => await notesTable.Truncate();
}