using MediatR;
using Reminy.Core.Domain.Entity;
using Reminy.Core.DomainServices.Notes.Commands.Create.Contracts;
using Reminy.Core.DomainServices.Notes.Ports;

namespace Reminy.Core.DomainServices.Notes.Commands.Create;

internal sealed class CreateNoteHandler(INoteStore noteStore) : IRequestHandler<CreateNoteCommand, Note>
{
    public async Task<Note> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await noteStore.Create(request.CreateNote, cancellationToken);

        return note;
    }
}