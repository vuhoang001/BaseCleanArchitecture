namespace Application.Interfaces;

public interface IUserContext
{
    string? UserId { get; set; }
    string? Email { get; set; }
    string? UserName { get; set; }
}