using MediatR;
using Reminy.Core.Domain.Entity;
using Reminy.Core.DomainServices.Notes.Commands.Read.Contracts;
using Reminy.Core.DomainServices.Notes.Ports;

namespace Reminy.Core.DomainServices.Notes.Commands.Read;

internal sealed class ReadNoteHandler(INoteStore noteStore) : IRequestHandler<ReadNoteCommand, Note?>
{
    public async Task<Note?> Handle(ReadNoteCommand request, CancellationToken cancellationToken)
    {
        Note? note;
        
        try
        {
            note = await noteStore.Get(request.NoteId, cancellationToken);
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
        
        return note;
    }
}