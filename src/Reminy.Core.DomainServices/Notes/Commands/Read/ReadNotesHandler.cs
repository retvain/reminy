using MediatR;
using Reminy.Core.Domain.Entity;
using Reminy.Core.DomainServices.Notes.Commands.Read.Contracts;
using Reminy.Core.DomainServices.Notes.Ports;

namespace Reminy.Core.DomainServices.Notes.Commands.Read;

internal sealed class ReadNotesHandler(INoteStore noteStore) : IRequestHandler<ReadNotesCommand, IReadOnlyCollection<Note>>
{
    public Task<IReadOnlyCollection<Note>> Handle(ReadNotesCommand request, CancellationToken cancellationToken)
    {
        var notes = noteStore.Get(cancellationToken);

        return notes;
    }
}