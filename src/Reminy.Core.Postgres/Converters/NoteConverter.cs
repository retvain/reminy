using Reminy.Core.Domain.Entity;
using Reminy.Core.Postgres.Models;

namespace Reminy.Core.Postgres.Converters;

internal static class NoteConverter
{
    public static Note ToDomain(NoteRaw noteRaw)
    {
        return new Note(
            id: noteRaw.id,
            title: noteRaw.title,
            content: noteRaw.content,
            tags: Array.Empty<Tag>());
    }

    public static IReadOnlyCollection<Note> ToDomain(IEnumerable<NoteRaw> notesRaw)
        => notesRaw.Select(ToDomain).ToArray();
}