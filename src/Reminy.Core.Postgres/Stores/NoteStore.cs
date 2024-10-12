using Dapper;
using Reminy.Core.Domain.Entity;
using Reminy.Core.DomainServices.Notes.Commands.Create.Models;
using Reminy.Core.DomainServices.Notes.Commands.Update.Models;
using Reminy.Core.DomainServices.Notes.Ports;
using Reminy.Core.Postgres.Contracts;
using Reminy.Core.Postgres.Converters;
using Reminy.Core.Postgres.Models;

namespace Reminy.Core.Postgres.Stores;

internal sealed class NoteStore(IConnectionFactory connectionFactory) : INoteStore
{
    public async Task Create(CreateNote createNote, CancellationToken cancellationToken)
    {
        const string query = @"
            INSERT INTO
                notes
                (title, content)
            VALUES (@Title, @Content)
            RETURNING 
            id, title, content";

        var commandDefinition = new CommandDefinition(
            query,
            new
            {
                Title = createNote.Title,
                Content = createNote.Content
            },
            cancellationToken: cancellationToken);

        await using var connection = connectionFactory.Create();
        await connection.QueryAsync(commandDefinition);
    }

    public async Task Update(UpdateNote updateNote, CancellationToken cancellationToken)
    {
        const string query = @"
            UPDATE notes
            SET 
                title = @Title,
                content = @Content
            WHERE id = @Id
            RETURNING id";

        var commandDefinition = new CommandDefinition(
            query,
            new
            {
                Id = updateNote.Id,
                Title = updateNote.Title,
                Content = updateNote.Content
            },
            cancellationToken: cancellationToken);

        await using var connection = connectionFactory.Create();
        var noteId = await connection.QueryFirstAsync<long?>(commandDefinition);

        if (!noteId.HasValue)
            throw new KeyNotFoundException($"note with id {updateNote.Id} not found");
    }

    public async Task<Note> Get(long noteId, CancellationToken cancellationToken)
    {
        const string query = @"
            SELECT
                id, title, content
            FROM 
                notes
            WHERE id = @Id";

        var commandDefinition = new CommandDefinition(
            query,
            new
            {
                Id = noteId
            },
            cancellationToken: cancellationToken);

        await using var connection = connectionFactory.Create();
        var noteRaw = await connection.QueryFirstOrDefaultAsync<NoteRaw?>(commandDefinition);

        if (noteRaw == null)
            throw new KeyNotFoundException($"note with id {noteId} not found");

        var note = NoteConverter.ToDomain(noteRaw);

        return note;
    }

    public async Task<IReadOnlyCollection<Note>> Get(CancellationToken cancellationToken)
    {
        const string query = @"
            SELECT
                id, title, content
            FROM 
                notes";

        var commandDefinition = new CommandDefinition(
            query,
            cancellationToken: cancellationToken);

        await using var connection = connectionFactory.Create();
        var notesRaw = await connection.QueryAsync<NoteRaw>(commandDefinition);

        var notes = NoteConverter.ToDomain(notesRaw);

        return notes;
    }

    public async Task Delete(long noteId, CancellationToken cancellationToken)
    {
        const string query = @"
            DELETE FROM notes
            WHERE id = @Id";

        var commandDefinition = new CommandDefinition(
            query,
            new
            {
                Id = noteId
            },
            cancellationToken: cancellationToken);

        await using var connection = connectionFactory.Create();
        await connection.QueryAsync(commandDefinition);
    }
}