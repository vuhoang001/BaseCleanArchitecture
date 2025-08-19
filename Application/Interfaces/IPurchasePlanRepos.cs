namespace Application.Interfaces;

public interface IPurchasePlanRepos
{
    Task<bool> CreateAsync(Domain.Entities.PurchasePlan dto);
    
    Task<Domain.Entities.PurchasePlan?> FindByCodeAsync(string code);
}