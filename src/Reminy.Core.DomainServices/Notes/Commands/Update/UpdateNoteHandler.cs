using MediatR;
using Reminy.Core.DomainServices.Notes.Commands.Update.Contracts;
using Reminy.Core.DomainServices.Notes.Ports;

namespace Reminy.Core.DomainServices.Notes.Commands.Update;

internal sealed class UpdateNoteHandler(INoteStore noteStore) : IRequestHandler<UpdateNoteCommand, Unit>
{
    public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        await noteStore.Update(request.UpdateNote, cancellationToken);

        return Unit.Value;
    }
}