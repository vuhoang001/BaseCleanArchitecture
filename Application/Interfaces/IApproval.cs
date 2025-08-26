namespace Application.Interfaces;

public interface IApproval
{
    Task<bool> CreateApprovalRequest();
}