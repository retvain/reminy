using MediatR;
using Reminy.Core.DomainServices.Note.Commands.Create.Models;

namespace Reminy.Core.DomainServices.Note.Commands.Create.Contracts;

using DomainNote = Domain.Entity.Note;

public sealed class CreateNoteCommand(CreateNote createNote) : IRequest<DomainNote>
{
    public CreateNote CreateNote { get; } = createNote;
}