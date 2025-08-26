using Application.Dtos;

namespace Application.Handlers.User.Commands.Register;

public record RegisterCommand(CreateRegisterRequest Login) : ICommand<string>;