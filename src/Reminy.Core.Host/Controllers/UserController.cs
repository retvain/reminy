using Microsoft.AspNetCore.Mvc;
using Reminy.Core.Host.Contracts;

namespace Reminy.Core.Host.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public sealed class UserController : ControllerBase
{
    [HttpPost("register")]
    public OkResult RegisterUser([FromBody] RegisterUserRequestDto registerUserRequest)
    {

        return Ok();
    }
}