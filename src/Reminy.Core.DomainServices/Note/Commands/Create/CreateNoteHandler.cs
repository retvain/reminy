using MediatR;
using Reminy.Core.DomainServices.Note.Commands.Create.Contracts;
using Reminy.Core.DomainServices.Note.Commands.Create.Ports;

namespace Reminy.Core.DomainServices.Note.Commands.Create;

using DomainNote = Domain.Entity.Note;

internal sealed class CreateNoteHandler(INoteStore noteStore) : IRequestHandler<CreateNoteCommand, DomainNote>
{
    public async Task<DomainNote> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await noteStore.Create(request.CreateNote);

        return note;
    }
}