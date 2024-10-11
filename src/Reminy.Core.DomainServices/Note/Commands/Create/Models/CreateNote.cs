namespace Reminy.Core.DomainServices.Note.Commands.Create.Models;

public sealed record CreateNote(
    string Title,
    string Content);