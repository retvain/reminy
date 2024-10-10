namespace Reminy.Core.Domain.Entity;

public sealed class Note(
    long id,
    string header,
    string content,
    IReadOnlyCollection<Tag> tags)
{
    public long Id { get; private set; } = id;

    public string Header { get; private set; } = header;

    public string Content { get; private set; } = content;

    public IReadOnlyCollection<Tag> Tags { get; private set; } = tags;

    public void SetHeader(string header)
        => Header = header;

    public void SetBody(string body)
        => Content = body;

    public void SetTags(IReadOnlyCollection<Tag> tags)
        => Tags = tags;
}