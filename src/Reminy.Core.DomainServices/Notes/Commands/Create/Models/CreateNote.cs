namespace Reminy.Core.DomainServices.Notes.Commands.Create.Models;

public sealed record CreateNote(
    string Title,
    string Content);