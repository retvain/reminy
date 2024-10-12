using Reminy.Core.DomainServices.Note.Commands.Create.Models;
using Reminy.Core.DomainServices.Note.Commands.Update.Models;

namespace Reminy.Core.DomainServices.Note.Ports;

using DomainNote = Domain.Entity.Note;

public interface INoteStore
{
    Task<DomainNote> Create(CreateNote createNote, CancellationToken cancellationToken);
    Task<DomainNote> Update(UpdateNote updateNote, CancellationToken cancellationToken);
}