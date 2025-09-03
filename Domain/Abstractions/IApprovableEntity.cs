using Domain.Enums;

namespace Domain.Abstractions;

public interface IApprovableEntity
{
    int EntityId { get; set; }
    string EntityType { get; set; }
    ApprovalStatus ApprStatus { get; set; }
    void ApplyApproval();
    void ApplyRejection(string reason);
}