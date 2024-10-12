using MediatR;
using Reminy.Core.DomainServices.Note.Commands.Update.Contracts;
using Reminy.Core.DomainServices.Note.Ports;

namespace Reminy.Core.DomainServices.Note.Commands.Update;

using DomainNote = Domain.Entity.Note;

internal sealed class UpdateNoteHandler(INoteStore noteStore) : IRequestHandler<UpdateNoteCommand, DomainNote?>
{
    public async Task<DomainNote?> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        DomainNote? note;

        try
        {
            note = await noteStore.Update(request.UpdateNote, cancellationToken);
        }
        catch (KeyNotFoundException)
        {
            return null;
        }

        return note;
    }
}