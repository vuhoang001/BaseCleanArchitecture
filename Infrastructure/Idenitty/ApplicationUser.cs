using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Idenitty;

public class ApplicationUser : IdentityUser<int>
{
    public bool IsActive { get; set; }
}