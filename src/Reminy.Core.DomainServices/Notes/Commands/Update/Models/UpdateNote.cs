namespace Reminy.Core.DomainServices.Notes.Commands.Update.Models;

public sealed record UpdateNote(
    long Id,
    string Title,
    string Content);