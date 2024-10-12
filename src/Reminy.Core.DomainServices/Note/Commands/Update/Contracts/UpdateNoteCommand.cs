using MediatR;
using Reminy.Core.DomainServices.Note.Commands.Update.Models;

namespace Reminy.Core.DomainServices.Note.Commands.Update.Contracts;

using DomainNote = Domain.Entity.Note;

public sealed class UpdateNoteCommand(UpdateNote updateNote) : IRequest<DomainNote?>
{
    public UpdateNote UpdateNote { get; } = updateNote;
}