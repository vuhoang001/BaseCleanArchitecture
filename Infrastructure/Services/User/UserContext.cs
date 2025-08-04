using Application.Interfaces;

namespace Infrastructure.Services.User;

public class UserContext : IUserContext
{
    public string? UserId { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
}