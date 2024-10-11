using Dapper;
using Reminy.Core.Domain.Entity;
using Reminy.Core.DomainServices.Note.Commands.Create.Models;
using Reminy.Core.DomainServices.Note.Commands.Create.Ports;
using Reminy.Core.Postgres.Contracts;
using Reminy.Core.Postgres.Converters;
using Reminy.Core.Postgres.Models;

namespace Reminy.Core.Postgres.Stores;

internal sealed class NoteStore(IConnectionFactory connectionFactory) : INoteStore
{
    public async Task<Note> Create(CreateNote createNote)
    {
        const string query = @"
            INSERT INTO
                Notes
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
            });

        await using var connection = connectionFactory.Create();
        var noteRaw = await connection.QueryFirstAsync<NoteRaw>(commandDefinition);

        return NoteConverter.ToDomain(noteRaw);
    }


}