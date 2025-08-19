using Application.Data;
using Application.Interfaces;

namespace Infrastructure.Services.PurchasePlan;

public class PurchasePlanRepos(IApplicationDbContext dbContext) : IPurchasePlanRepos
{
    public async Task<bool> CreateAsync(Domain.Entities.PurchasePlan dto)
    {
        dbContext.PurchasePlan.Add(dto);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public Task<Domain.Entities.PurchasePlan?> FindByCodeAsync(string code)
    {
        var result = dbContext.PurchasePlan
            .FirstOrDefaultAsync(x => x.DocCode == code);
        return result;
    }
}