using MediatR;
using Reminy.Core.Domain.Entity;
using Reminy.Core.DomainServices.Notes.Commands.Read.Contracts;

namespace Reminy.Core.DomainServices.Notes.Commands.Read;

internal sealed class ReadNotesHandler : IRequestHandler<ReadNotesCommand, IReadOnlyCollection<Note>>
{
    public Task<IReadOnlyCollection<Note>> Handle(ReadNotesCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}