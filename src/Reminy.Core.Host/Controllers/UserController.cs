using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reminy.Core.DomainServices.User.Commands.Register.Contracts;
using Reminy.Core.Host.Dto;

namespace Reminy.Core.Host.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public sealed class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterUserRequestDto registerUserRequest)
    {
        var registerUserCommand = new RegisterUserCommand(
            email: registerUserRequest.Email,
            firstName: registerUserRequest.FirstName,
            lastName: registerUserRequest.LastName,
            password: registerUserRequest.Password);

        await mediator.Send(registerUserCommand);

        return Ok();
    }
}