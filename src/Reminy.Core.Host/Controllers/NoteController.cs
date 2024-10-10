using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reminy.Core.DomainServices.Note.Commands.Create.Contracts;
using Reminy.Core.Host.Dto;

namespace Reminy.Core.Host.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public sealed class NoteController(IMediator mediator) : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult> CreateNote([FromBody] CreateNoteRequestDto requestDto)
    {
        var createNoteCommand = new CreateNoteCommand(
            header: requestDto.Header,
            content: requestDto.Content);

        var note = await mediator.Send(createNoteCommand);

        return Ok();
    }
}