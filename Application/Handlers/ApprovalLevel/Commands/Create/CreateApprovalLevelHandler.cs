using Application.Interfaces;
using Application.Interfaces.Base;
using Shared.ExceptionBase;

namespace Application.Handlers.ApprovalLevel.Commands.Create;

public class CreateApprovalLevelHandler(
    IApprovalLevelRepos approvalLevelRepos,
    ICodeGeneration codeGeneration,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
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

        var approvalLevel = Domain.Entities.ApprovalLevel.Create(code, request.ApprovalLevel.ApprovalLevelName,
            request.ApprovalLevel.IsActive, request.ApprovalLevel.NumberOfApproval,
            request.ApprovalLevel.NumberOfRejection);

        await approvalLevelRepos.CreateAsync(approvalLevel);

        await unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }
}