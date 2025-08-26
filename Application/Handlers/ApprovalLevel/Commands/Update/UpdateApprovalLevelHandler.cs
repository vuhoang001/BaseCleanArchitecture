using Application.Interfaces;
using Shared.ExceptionBase;

namespace Application.Handlers.ApprovalLevel.Commands.Update;

public class UpdateApprovalLevelHandler(IApprovalLevelRepos repos, IUserRepository userRepository)
    : ICommandHandler<UpdateApprovalLevelCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateApprovalLevelCommand request, CancellationToken cancellationToken)
    {
        var result = await repos.GetByIdAsync(request.Id);
        if (result is null) return Result<bool>.Failure($"Không tìm thấy đơn hàng {request.Id}");

        var userIds      = request.ApprovalLevel.ApprovalLevelUserApprs;
        var usersExisted = await userRepository.GetUsersByIds(userIds);

        if (usersExisted == null || usersExisted.Count() != userIds.Count())
        {
            var existingIds = usersExisted?.Select(u => u.Id).ToList() ?? new List<int>();
            var missingIds  = userIds.Except(existingIds).ToList();
            return Result<bool>.Failure($"Người dùng không tồn tại: {string.Join(", ", missingIds)}");
        }

        result.Update(request.ApprovalLevel.ApprovalLevelName, request.ApprovalLevel.IsActive,
            request.ApprovalLevel.NumberOfApproval, request.ApprovalLevel.NumberOfRejection);
        result.UpdateUser(request.ApprovalLevel.ApprovalLevelUserApprs);

        await repos.UpdateAsync(result);


        return Result<bool>.Success(true);
    }
}