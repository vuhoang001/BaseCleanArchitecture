using Domain.Entities;

namespace Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(Domain.Entities.User user);
    bool   ValidateToken(string token);
}