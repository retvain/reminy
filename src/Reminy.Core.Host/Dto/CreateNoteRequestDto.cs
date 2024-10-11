namespace Reminy.Core.Host.Dto;

public sealed class CreateNoteRequestDto
{
    public required string Title { get; set; }

    public required string Content { get; set; }
}