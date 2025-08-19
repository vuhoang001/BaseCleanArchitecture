namespace Domain.Entities;

public class User : Entity<string>
{
    public string UserName { get; private set; }
    public bool IsActive { get; private set; }
    public string Email { get; private set; }

    public User(string? id, string userName, string email)
    {
        if (userName == null || email == null) throw new ArgumentNullException();
        if (id is null) Id = Guid.NewGuid().ToString();
        else Id            = id;
        UserName = userName;
        Email    = email;
    }

    public void Active()
    {
        IsActive = true;
    }

    public void Deactive()
    {
        IsActive = false;
    }
}