using MediatR;

namespace Reminy.Core.DomainServices.Note.Commands.Create.Contracts;

using DomainNote = Domain.Entity.Note;

public sealed class CreateNoteCommand(
    string header,
    string content) : IRequest<DomainNote>
{
    public string Header { get; } = header;
    public string Content { get; } = content;
}