namespace Reminy.Core.DomainServices.Note.Commands.Update.Models;

public sealed record UpdateNote(
    long Id,
    string Title,
    string Content);