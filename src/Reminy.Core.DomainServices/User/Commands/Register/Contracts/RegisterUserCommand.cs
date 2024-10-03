using MediatR;

namespace Reminy.Core.DomainServices.User.Commands.Register.Contracts;

public sealed class RegisterUserCommand(
    string email,
    string firstName,
    string lastName,
    string password)
    : IRequest<Unit>
{
    public string Email { get; } = email;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public string Password { get; } = password;
}