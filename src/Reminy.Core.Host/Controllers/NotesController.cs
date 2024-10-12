using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reminy.Core.DomainServices.Notes.Commands.Read.Contracts;
using Reminy.Core.DomainServices.Notes.Commands.Read.Models;
using Reminy.Core.Host.Dto;

namespace Reminy.Core.Host.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public sealed class NotesController(IMediator mediator) : ControllerBase
{
    [HttpPost("get")]
    public async Task<IActionResult> Update([FromBody] GetNotesRequestDto requestDto)
    {
        var readNotes = new ReadNotes();
        var readNoteCommand = new ReadNotesCommand(readNotes);

        var notes = await mediator.Send(readNoteCommand);

        return Ok(notes);
    }
}