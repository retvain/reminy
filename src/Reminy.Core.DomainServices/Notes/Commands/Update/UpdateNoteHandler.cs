using MediatR;
using Reminy.Core.Domain.Entity;
using Reminy.Core.DomainServices.Notes.Commands.Update.Contracts;
using Reminy.Core.DomainServices.Notes.Ports;

namespace Reminy.Core.DomainServices.Notes.Commands.Update;

internal sealed class UpdateNoteHandler(INoteStore noteStore) : IRequestHandler<UpdateNoteCommand, Note?>
{
    public async Task<Note?> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        Note? note;

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