namespace Reminy.Core.Host.Dto;

public sealed class CreateNoteRequestDto
{
    public required string Header { get; set; }

    public required string Content { get; set; }
}