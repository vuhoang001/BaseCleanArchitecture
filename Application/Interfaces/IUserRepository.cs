namespace Application.Interfaces;

public interface IUserRepository
{
    Task<Domain.Entities.User?> GetByIdAsync(int id);
    Task<Domain.Entities.User?> GetByEmailAsync(string email);

    Task<List<Domain.Entities.User>?> GetUsersByIds(List<int> ids);

    Task CreateAsync(Domain.Entities.User user, string password);
    Task UpdateAsync(Domain.Entities.User user);
    Task DeleteAsync(Domain.Entities.User user);

    Task<bool> CheckPasswordAsync(Domain.Entities.User user, string password);
}