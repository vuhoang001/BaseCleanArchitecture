using Domain.Abstractions;

namespace Application.Interfaces.Approval;

public interface IApprovalEngine
{
    Task CreateAsync(IApprovableEntity entity, CancellationToken ct);
    Task SubmitAsync(IApprovableEntity entity, CancellationToken ct);
    Task ApproveAsync(IApprovableEntity entity, string approverId, CancellationToken ct);
    Task RejectAsync(IApprovableEntity entity, string approverId, string reason, CancellationToken ct);
}