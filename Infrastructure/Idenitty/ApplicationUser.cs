using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Idenitty;

public class ApplicationUser : IdentityUser<string>
{
    public bool IsActive { get; set; }
}