using Application.Interfaces;

namespace Application.Handlers.User.Commands.Register;

public class RegisterHandler(IUserRepository userRepository) : IRequestHandler<RegisterCommand, string>
{
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existed = await userRepository.GetByEmailAsync(request.Login.Email);
        if (existed is not null) throw new UnauthorizedAccessException();

        var user = new Domain.Entities.User(request.Login.Username, request.Login.Email);

        await userRepository.CreateAsync(user, request.Login.Password);

        return user.Email;
    }
}