using Infrastructure.Data.Interceptors;

namespace Infrastructure.Idenitty.Mapper;

public static class UserMapper
{
    public static ApplicationUser ToIdentity(User user)
    {
        return new ApplicationUser
        {
            Id       = user.Id,
            UserName = user.UserName,
            Email    = user.Email,
            IsActive = user.IsActive,
        };
    }

    public static User ToDomain(ApplicationUser aUser)
    {
        var user = new User(aUser.Id, aUser.UserName!, aUser.Email!);

        if (aUser.IsActive) user.Active();
        else user.Deactive();

        return user;
    }
}