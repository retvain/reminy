using MediatR;
using Reminy.Core.Domain.Entity;
using Reminy.Core.DomainServices.Notes.Commands.Read.Models;

namespace Reminy.Core.DomainServices.Notes.Commands.Read.Contracts;

public sealed class ReadNotesCommand(ReadNotes readNotes) : IRequest<IReadOnlyCollection<Note>>
{
    public ReadNotes ReadNotes { get; } = readNotes;
}