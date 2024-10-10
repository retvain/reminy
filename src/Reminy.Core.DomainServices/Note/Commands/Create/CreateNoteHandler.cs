using MediatR;
using Reminy.Core.DomainServices.Note.Commands.Create.Contracts;

namespace Reminy.Core.DomainServices.Note.Commands.Create;

using DomainNote = Domain.Entity.Note;

internal sealed class CreateNoteHandler : IRequestHandler<CreateNoteCommand, DomainNote>
{
    public Task<DomainNote> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}