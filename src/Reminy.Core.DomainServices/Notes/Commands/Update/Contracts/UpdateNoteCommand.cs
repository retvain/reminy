using MediatR;
using Reminy.Core.DomainServices.Notes.Commands.Update.Models;

namespace Reminy.Core.DomainServices.Notes.Commands.Update.Contracts;

public sealed class UpdateNoteCommand(UpdateNote updateNote) : IRequest<Unit>
{
    public UpdateNote UpdateNote { get; } = updateNote;
}