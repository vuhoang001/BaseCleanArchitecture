using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Idenitty;
using Infrastructure.Idenitty.Mapper;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.User;

public class UserRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) : IUserRepository
{
    public async Task<Domain.Entities.User?> GetByIdAsync(string id)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null) return null;

        var result = UserMapper.ToDomain(user);
        return result;
    }

    public async Task<Domain.Entities.User?> GetByEmailAsync(string email)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user is null) return null;

        var result = UserMapper.ToDomain(user);
        return result;
    }

    public async Task CreateAsync(Domain.Entities.User user, string password)
    {
        var aUser  = UserMapper.ToIdentity(user);
        var result = await userManager.CreateAsync(aUser, password);
        if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
    }

    public async Task UpdateAsync(Domain.Entities.User user)
    {
        var aUser  = UserMapper.ToIdentity(user);
        var result = await userManager.UpdateAsync(aUser);
        if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
    }

    public async Task DeleteAsync(Domain.Entities.User user)
    {
        var aUser  = UserMapper.ToIdentity(user);
        var result = await userManager.DeleteAsync(aUser);
        if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
    }

    public async Task<bool> CheckPasswordAsync(Domain.Entities.User user, string password)
    {
        var appUser = await userManager.FindByIdAsync(user.Id);
        if (appUser == null) return false;
        return await userManager.CheckPasswordAsync(appUser, password);
    }
}