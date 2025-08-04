using Domain.Enums;

namespace Domain.Interfaces;

public interface IApprovalDocument
{
   ApprovalStatus ApprovalStatus { get; }
   void Approve();
   void Reject();
}