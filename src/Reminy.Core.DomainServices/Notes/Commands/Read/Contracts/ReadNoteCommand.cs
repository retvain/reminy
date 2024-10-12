using MediatR;
using Reminy.Core.Domain.Entity;

namespace Reminy.Core.DomainServices.Notes.Commands.Read.Contracts;

public sealed class ReadNoteCommand(long noteId) : IRequest<Note?>
{
    public long NoteId { get; } = noteId;
}