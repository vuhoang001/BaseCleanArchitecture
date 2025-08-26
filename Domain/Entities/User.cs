namespace Domain.Entities;

public class User : Entity<int>
{
    public string UserName { get; private set; }
    public bool IsActive { get; private set; }
    public string Email { get; private set; }

    public User(string userName, string email)
    {
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