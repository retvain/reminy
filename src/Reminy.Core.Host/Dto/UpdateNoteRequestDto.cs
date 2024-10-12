namespace Reminy.Core.Host.Dto;

public sealed class UpdateNoteRequestDto
{
    public required long Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
}