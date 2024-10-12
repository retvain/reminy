using MediatR;
using Reminy.Core.DomainServices.Notes.Commands.Delete.Contracts;
using Reminy.Core.DomainServices.Notes.Ports;

namespace Reminy.Core.DomainServices.Notes.Commands.Delete;

internal sealed class DeleteNoteHandler(INoteStore noteStore) : IRequestHandler<DeleteNoteCommand, Unit>
{
    public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        await noteStore.Delete(request.NoteId, cancellationToken);

        return Unit.Value;
    }
}