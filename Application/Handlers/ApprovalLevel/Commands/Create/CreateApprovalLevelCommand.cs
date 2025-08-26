using Shared.ExceptionBase;

namespace Application.Handlers.ApprovalLevel.Commands.Create;

public record CreateApprovalLevelCommand(CreateApprovalLevel ApprovalLevel) : ICommand<Result<bool>>
{
}

public record CreateApprovalLevel
{
    public string ApprovalLevelCode { get; set; } = default!;
    public string ApprovalLevelName { get; set; } = default!;
    public bool IsActive { get; set; }

    public int NumberOfApproval { get; set; }
    public int NumberOfRejection { get; set; }

    public List<int> ApprovalLevelUserApprs { get; set; } = new();
}