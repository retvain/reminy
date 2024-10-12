using MediatR;

namespace Reminy.Core.DomainServices.Notes.Commands.Delete.Contracts;

public sealed class DeleteNoteCommand(long noteId) : IRequest<Unit>
{
    public long NoteId { get; } = noteId;
}