using MediatR;
using Reminy.Core.DomainServices.Notes.Commands.Create.Contracts;
using Reminy.Core.DomainServices.Notes.Ports;

namespace Reminy.Core.DomainServices.Notes.Commands.Create;

internal sealed class CreateNoteHandler(INoteStore noteStore) : IRequestHandler<CreateNoteCommand, Unit>
{
    public async Task<Unit> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        await noteStore.Create(request.CreateNote, cancellationToken);

        return Unit.Value;
    }
}