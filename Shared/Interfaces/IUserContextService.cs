namespace Shared.Interfaces;

public interface IUserContextService
{
    List<string> GetCurrencyUserRole();
    List<string>       GetCurrencyUserClaims();
}