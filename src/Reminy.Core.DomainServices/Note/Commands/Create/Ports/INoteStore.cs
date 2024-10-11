using Reminy.Core.DomainServices.Note.Commands.Create.Models;

namespace Reminy.Core.DomainServices.Note.Commands.Create.Ports;

using DomainNote = Domain.Entity.Note;

public interface INoteStore
{
    Task<DomainNote> Create(CreateNote createNote);
}