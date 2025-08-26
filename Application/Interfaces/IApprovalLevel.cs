using Application.Handlers.ApprovalLevel.Commands.Create;
using Domain.Entities;

namespace Application.Interfaces;

public interface IApprovalLevelRepos
{
    Task CreateAsync(ApprovalLevel approvalLevel);

    Task<ApprovalLevel?> GetByIdAsync(string id);


    Task UpdateAsync(ApprovalLevel approvalLevel);

    Task DeleteAsync(string id);
}