using Application.Interfaces;

namespace Application.User.Commands.Register;

public class RegisterHandler(IUserRepository userRepository) : IRequestHandler<RegisterCommand, string>
{
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existed = await userRepository.GetByEmailAsync(request.Login.Email);
        if (existed is not null) throw new UnauthorizedAccessException();

        var user = new Domain.Entities.User(null, request.Login.Email, request.Login.Username);

        await userRepository.CreateAsync(user, request.Login.Password);

        return user.Email;
    }
}