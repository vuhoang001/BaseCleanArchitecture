namespace Application.Dtos;

public record LoginRequest
{
    /// <summary>
    /// Đăng nhập bằng email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Mật khẩu
    /// </summary>
    public string Password { get; set; } = string.Empty;
}

public record LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}