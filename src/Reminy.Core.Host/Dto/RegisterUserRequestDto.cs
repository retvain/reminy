namespace Reminy.Core.Host.Dto;

public sealed class RegisterUserRequestDto
{
    public required string Email { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Password { get; set; }
}