using Reminy.Core.Domain.Entity;
using Reminy.Core.DomainServices.Notes.Commands.Create.Models;
using Reminy.Core.DomainServices.Notes.Commands.Update.Models;

namespace Reminy.Core.DomainServices.Notes.Ports;

public interface INoteStore
{
    Task<Note> Create(CreateNote createNote, CancellationToken cancellationToken);
    Task<Note> Update(UpdateNote updateNote, CancellationToken cancellationToken);
    Task<Note> Get(long noteId, CancellationToken cancellationToken);
}