namespace Reminy.Core.Domain.Entity;

public sealed class Tag
{
    public string Value { get; private set; }

    public Tag(string value)
    {
        Value = value;
    }

    public void Set(string value)
        => Value = value;
}