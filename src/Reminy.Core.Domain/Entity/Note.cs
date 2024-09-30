namespace Reminy.Core.Domain.Entity;

public sealed class Note
{
    public long Id { get; private set; }

    public string Header { get; private set; }

    public string Body { get; private set; }

    public IReadOnlyCollection<Tag> Tags { get; private set; }

    public Note(long id, string header, string body, IReadOnlyCollection<Tag> tags)
    {
        Id = id;
        Header = header;
        Body = body;
        Tags = tags;
    }

    public void SetHeader(string header)
        => Header = header;

    public void SetBody(string body)
        => Body = body;

    public void SetTags(IReadOnlyCollection<Tag> tags)
        => Tags = tags;
}