using Application.Dtos.Response;
using Application.Interfaces;
using Shared.ExceptionBase;

namespace Application.User.Commands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, Result<LoginRegisteResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService     _jwtService;

    public LoginHandler(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService     = jwtService;
    }

    public async Task<Result<LoginRegisteResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var existed = await _userRepository.GetByEmailAsync(request.LoginInfo.Email);
        if (existed is null) return Result<LoginRegisteResponse>.Failure("Tài khoản và mật khẩu không hợp nệ");

        var validPassword = await _userRepository.CheckPasswordAsync(existed, request.LoginInfo.Password);
        if (!validPassword) return Result<LoginRegisteResponse>.Failure("Tài khoản hoặc mật khẩu không hợp lệ");

        var token = _jwtService.GenerateToken(existed);

        return Result<LoginRegisteResponse>.Success(new LoginRegisteResponse
        {
            AccessToken = token,
            ExpiresIn   = 30
        });
    }
}