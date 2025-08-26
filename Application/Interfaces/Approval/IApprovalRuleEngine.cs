using Domain.Abstractions;

namespace Application.Interfaces.Approval;

public interface IApprovalRuleEngine
{
    Task<bool> BuildApprovalPlanAsync(IApprovableEntity entity, CancellationToken ct);
}