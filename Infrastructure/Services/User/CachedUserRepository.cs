using System.Text.Json;
using Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Services.User;

public class CachedUserRepository(IUserRepository userRepository, IDistributedCache cache) : IUserRepository
{
    public async Task<Domain.Entities.User?> GetByIdAsync(int id)
    {
        var cached = await cache.GetStringAsync(id.ToString());
        if (!string.IsNullOrWhiteSpace(cached))
            return JsonSerializer.Deserialize<Domain.Entities.User>(cached);

        var user = await userRepository.GetByIdAsync(id);
        if (user != null)
        {
            await cache.SetStringAsync(
                id.ToString(),
                JsonSerializer.Serialize(user),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
        }

        return user;
    }

    public async Task<Domain.Entities.User?> GetByEmailAsync(string email)
    {
        var cached = await cache.GetStringAsync(email);

        if (!string.IsNullOrWhiteSpace(cached))
            return JsonSerializer.Deserialize<Domain.Entities.User>(cached);

        var user = await userRepository.GetByEmailAsync(email);
        if (user != null)
        {
            await cache.SetStringAsync(
                email,
                JsonSerializer.Serialize(user),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
        }

        return user;
    }

    public Task<List<Domain.Entities.User>?> GetUsersByIds(List<int> ids)
    {
        return userRepository.GetUsersByIds(ids);
    }

    public async Task CreateAsync(Domain.Entities.User user, string password)
    {
        await userRepository.CreateAsync(user, password);
        await cache.SetStringAsync(user.Id.ToString(), JsonSerializer.Serialize(user), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        });
    }

    public async Task UpdateAsync(Domain.Entities.User user)
    {
        await userRepository.UpdateAsync(user);

        await cache.SetStringAsync(user.Id.ToString(), JsonSerializer.Serialize(user), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        });
    }

    public async Task DeleteAsync(Domain.Entities.User user)
    {
        await userRepository.DeleteAsync(user);
        await cache.RemoveAsync(user.Id.ToString());
    }

    public async Task<bool> CheckPasswordAsync(Domain.Entities.User user, string password)
    {
        var resul = await userRepository.CheckPasswordAsync(user, password);
        return resul;
    }
}