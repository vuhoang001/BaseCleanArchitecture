using Application.Interfaces;
using Shared.ExceptionBase;

namespace Application.Handlers.ApprovalLevel.Commands.Create;

public class CreateApprovalLevelHandler(
    IApprovalLevelRepos approvalLevelRepos,
    ICodeGeneration codeGeneration,
    IUserRepository userRepository)
    : ICommandHandler<CreateApprovalLevelCommand, Result<bool>>
{
    public async
        Task<Result<bool>> Handle(CreateApprovalLevelCommand request, CancellationToken cancellationToken)
    {
        var code = await codeGeneration.GenerateCodeAsync<Domain.Entities.ApprovalLevel>(x => x.ApprovalLevelCode,
            "CPD");

        var userIds      = request.ApprovalLevel.ApprovalLevelUserApprs;
        var usersExisted = await userRepository.GetUsersByIds(userIds);
        if (usersExisted is null || usersExisted.Count() != userIds.Count())
        {
            var existingIds = usersExisted?.Select(u => u.Id).ToList() ?? new List<int>();
            var missingIds  = userIds.Except(existingIds).ToList();
            return Result<bool>.Failure($"Người dùng không tồn tại: {string.Join(", ", missingIds)}");
        }


        var approvalLevel = new Domain.Entities.ApprovalLevel(code,
            request.ApprovalLevel.ApprovalLevelName, request.ApprovalLevel.IsActive,
            request.ApprovalLevel.NumberOfApproval, request.ApprovalLevel.NumberOfRejection);
        approvalLevel.AddItem(request.ApprovalLevel.ApprovalLevelUserApprs);

        await approvalLevelRepos.CreateAsync(approvalLevel);
        return Result<bool>.Success(true);
    }
}