using Application.Dtos;
using Application.Dtos.Response;
using Shared.ExceptionBase;

namespace Application.Handlers.User.Commands.Login;

public record LoginCommand(LoginRequest LoginInfo) : ICommand<Result<LoginRegisteResponse>>;