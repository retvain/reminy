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
    public async Task<Note> Create(CreateNote createNote, CancellationToken cancellationToken)
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
        var noteRaw = await connection.QueryFirstAsync<NoteRaw>(commandDefinition);
        
        var note = NoteConverter.ToDomain(noteRaw);

        return note;
    }

    public async Task<Note> Update(UpdateNote updateNote, CancellationToken cancellationToken)
    {
        const string query = @"
            UPDATE notes
            SET 
                title = @Title,
                content = @Content
            WHERE id = @Id
            RETURNING id, title, content";

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
        var noteRaw = await connection.QueryFirstAsync<NoteRaw>(commandDefinition);

        if (noteRaw == null)
            throw new KeyNotFoundException($"note with id {updateNote.Id} not found");
        
        var note = NoteConverter.ToDomain(noteRaw);

        return note;
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
        var noteRaw = await connection.QueryFirstAsync<NoteRaw>(commandDefinition);

        if (noteRaw == null)
            throw new KeyNotFoundException($"note with id {noteId} not found");
        
        var note = NoteConverter.ToDomain(noteRaw); 

        return note;
    }
}