using MediatR;
using Reminy.Core.DomainServices.Notes.Commands.Create.Models;

namespace Reminy.Core.DomainServices.Notes.Commands.Create.Contracts;

public sealed class CreateNoteCommand(CreateNote createNote) : IRequest<Unit>
{
    public CreateNote CreateNote { get; } = createNote;
}