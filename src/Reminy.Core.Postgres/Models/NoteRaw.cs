namespace Reminy.Core.Postgres.Models;

internal sealed record NoteRaw(
    long id,
    string title,
    string content);