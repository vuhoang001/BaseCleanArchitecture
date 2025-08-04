using Application.Dtos;

namespace Application.User.Commands.Register;

public record RegisterCommand(RegisterDto Login) : ICommand<string>;