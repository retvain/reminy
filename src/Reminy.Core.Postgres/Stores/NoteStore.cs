using Dapper;
using Reminy.Core.Domain.Entity;
using Reminy.Core.DomainServices.Note.Commands.Create.Models;
using Reminy.Core.DomainServices.Note.Commands.Update.Models;
using Reminy.Core.DomainServices.Note.Ports;
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

        return NoteConverter.ToDomain(noteRaw);
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

        return NoteConverter.ToDomain(noteRaw);
    }
}