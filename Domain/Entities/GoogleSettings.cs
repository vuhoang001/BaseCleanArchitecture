namespace Domain.Entities;

public class GoogleSettings
{
    public const string SectionName = "GoogleSettings";
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;

    public string RedirectUri { get; set; } = null!;
}