using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reminy.Core.DomainServices.Note.Commands.Create.Contracts;
using Reminy.Core.DomainServices.Note.Commands.Create.Models;
using Reminy.Core.Host.Dto;

namespace Reminy.Core.Host.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public sealed class NoteController(IMediator mediator) : ControllerBase
{
    [HttpPost("create")]
    public async Task<OkObjectResult> CreateNote([FromBody] CreateNoteRequestDto requestDto)
    {
        var createNote = new CreateNote(Title: requestDto.Title, Content: requestDto.Content);
        var createNoteCommand = new CreateNoteCommand(createNote);

        var note = await mediator.Send(createNoteCommand);

        return Ok(note);
    }
}