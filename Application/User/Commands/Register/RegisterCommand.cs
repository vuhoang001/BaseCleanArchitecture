using Application.Dtos;

namespace Application.User.Commands.Register;

public record RegisterCommand(CreateRegisterRequest Login) : ICommand<string>;