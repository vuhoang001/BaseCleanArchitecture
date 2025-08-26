using Shared.ExceptionBase;

namespace Application.Handlers.ApprovalLevel.Commands.Update;

public record UpdateApprovalLevelCommand(string Id, UpdateApprovalLevel ApprovalLevel) : ICommand<Result<bool>>;

public record UpdateApprovalLevel
{
    public string ApprovalLevelName { get; set; } = default!;
    public bool IsActive { get; set; }
    public int NumberOfApproval { get; set; }
    public int NumberOfRejection { get; set; }
    public List<int> ApprovalLevelUserApprs { get; set; } = new();
}