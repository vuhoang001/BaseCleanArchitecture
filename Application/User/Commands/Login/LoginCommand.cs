using Application.Dtos;

namespace Application.User.Commands.Login;

public record LoginCommand(LoginRequest LoginInfo) : ICommand<LoginResponse>;