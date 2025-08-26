using Application.Interfaces;
using Application.Interfaces.Approval;
using Domain.Abstractions;

namespace Application.Services;

public class ApprovalEngine(IApprovalRuleEngine ruleEngine, IApproval approval) : IApprovalEngine
{
    public async Task CreateAsync(IApprovableEntity entity, CancellationToken ct)
    {
        var isNeedAppr = await ruleEngine.BuildApprovalPlanAsync(entity, ct);
        if (!isNeedAppr) return;

        await approval.CreateApprovalRequest();
    }

    public Task SubmitAsync(IApprovableEntity entity, CancellationToken ct)
    {
        
        entity.ApplyApproval();
        return Task.CompletedTask;
    }

    public Task ApproveAsync(IApprovableEntity entity, string approverId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task RejectAsync(IApprovableEntity entity, string approverId, string reason, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}