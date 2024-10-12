using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reminy.Core.DomainServices.Notes.Commands.Create.Contracts;
using Reminy.Core.DomainServices.Notes.Commands.Create.Models;
using Reminy.Core.DomainServices.Notes.Commands.Delete.Contracts;
using Reminy.Core.DomainServices.Notes.Commands.Read.Contracts;
using Reminy.Core.DomainServices.Notes.Commands.Update.Contracts;
using Reminy.Core.DomainServices.Notes.Commands.Update.Models;
using Reminy.Core.Host.Dto;

namespace Reminy.Core.Host.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public sealed class NoteController(IMediator mediator) : ControllerBase
{
    [HttpPost("create")]
    public async Task<OkObjectResult> Create([FromBody] CreateNoteRequestDto requestDto)
    {
        var createNote = new CreateNote(Title: requestDto.Title, Content: requestDto.Content);
        var createNoteCommand = new CreateNoteCommand(createNote);

        var note = await mediator.Send(createNoteCommand);

        return Ok(note);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UpdateNoteRequestDto requestDto)
    {
        var updateNote = new UpdateNote(Id: requestDto.Id, Title: requestDto.Title, Content: requestDto.Content);
        var updateNoteCommand = new UpdateNoteCommand(updateNote);

        var note = await mediator.Send(updateNoteCommand);

        if (note == null)
            return NotFound($"note with id {requestDto.Id} not found");

        return Ok(note);
    }

    [HttpPost("get")]
    public async Task<IActionResult> Update([FromBody] GetNoteRequestDto requestDto)
    {
        var readNoteCommand = new ReadNoteCommand(requestDto.Id);

        var note = await mediator.Send(readNoteCommand);

        if (note == null)
            return NotFound($"note with id {requestDto.Id} not found");

        return Ok(note);
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteNoteRequestDto requestDto)
    {
        var deleteNoteCommand = new DeleteNoteCommand(requestDto.Id);

        await mediator.Send(deleteNoteCommand);

        return Ok();
    }
}