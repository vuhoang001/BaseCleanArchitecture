using Application.Dtos;
using Application.Interfaces;

namespace Application.User.Commands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService     _jwtService;

    public LoginHandler(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService     = jwtService;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var existed = await _userRepository.GetByEmailAsync(request.LoginInfo.Email);
        if (existed is null) throw new UnauthorizedAccessException();

        var validPassword = await _userRepository.CheckPasswordAsync(existed, request.LoginInfo.Password);
        if (!validPassword) throw new UnauthorizedAccessException();

        var token = _jwtService.GenerateToken(existed);

        return new LoginResponse
        {
            AccessToken = token,
            ExpiresAt   = DateTime.UtcNow.AddMinutes(30),
        };
    }
}