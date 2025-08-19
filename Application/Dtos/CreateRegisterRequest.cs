namespace Application.Dtos;

public class CreateRegisterRequest
{
    /// <summary>
    /// Đăng nhập bằng email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}