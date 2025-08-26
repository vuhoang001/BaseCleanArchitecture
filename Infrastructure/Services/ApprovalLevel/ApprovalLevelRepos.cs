using Application.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Services.ApprovalLevel;

public class ApprovalLevelRepos(ApplicationDbContext context) : IApprovalLevelRepos
{
    public async Task CreateAsync(Domain.Entities.ApprovalLevel approvalLevel)
    {
        context.ApprovalLevel.Add(approvalLevel);
        await context.SaveChangesAsync();
    }

    public async Task<Domain.Entities.ApprovalLevel?> GetByIdAsync(string id)
    {
        var result = await context.ApprovalLevel.FirstOrDefaultAsync(x => x.Id == id);
        return result;
    }

    public async Task UpdateAsync(Domain.Entities.ApprovalLevel approvalLevel)
    {
        context.ApprovalLevel.Update(approvalLevel);
        await context.SaveChangesAsync();
    }

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }
}