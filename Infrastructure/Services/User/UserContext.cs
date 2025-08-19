using Application.Interfaces;
using Shared.Interfaces;

namespace Infrastructure.Services.User;

public class UserContext : IUserContext, IUserContextService
{
    public string? UserId { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }

    public List<string> GetCurrencyUserRole()
    {
        return ["Admin", "SuperAdmin"];
    }

    public List<string> GetCurrencyUserClaims()
    {
        return ["purchaserequest:create"];
    }
}