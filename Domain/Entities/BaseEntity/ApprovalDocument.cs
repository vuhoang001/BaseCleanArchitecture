using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities.BaseEntity;

public class ApprovalDocument : IApprovalDocument
{
    public ApprovalStatus ApprovalStatus { get; private set; }

    public ApprovalDocument(ApprovalStatus approvalStatus)
    {
        ApprovalStatus = approvalStatus;
    }

    public void Approve()
    {
        throw new NotImplementedException();
    }

    public void Reject()
    {
        throw new NotImplementedException();
    }
}