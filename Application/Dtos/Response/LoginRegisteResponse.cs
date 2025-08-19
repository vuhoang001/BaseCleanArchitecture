namespace Application.Dtos.Response;

public class LoginRegisteResponse
{
    public string? AccessToken { get; set; }
    public long ExpiresIn { get; set; }
}