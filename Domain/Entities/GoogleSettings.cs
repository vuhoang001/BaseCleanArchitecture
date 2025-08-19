namespace Domain.Entities;

public class GoogleSettings
{
    public const string SectionName = "GoogleSettings";
    public const string CallBackUrl = "/signin-google";
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
}