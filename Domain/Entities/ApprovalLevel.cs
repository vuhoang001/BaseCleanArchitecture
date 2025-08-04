namespace Domain.Entities;

public class ApprovalLevel : Aggregate<string>
{
    /// <summary>
    /// Mã cấp phê duyệt
    /// </summary>
    public string ApprovalLevelCode { get; private set; } = "";

    /// <summary>
    /// Mô tả
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Số lượng phê duyệt
    /// </summary>
    public int NumberAppr { get; set; }

    /// <summary>
    /// Số lượng từ chối
    /// </summary>
    public int NumberDecline { get; private set; }

    /// <summary>
    /// Trạng thái
    /// </summary>
    public bool IsActive { get; private set; }

    private readonly List<ApprovalLevelLine> _approvalLevelLines = new();
    public IReadOnlyList<ApprovalLevelLine> ApprovalLevelLines => _approvalLevelLines;

    private ApprovalLevel()
    {
    }

    public ApprovalLevel(string approvalLevelCode, string? description, int numberAppr, int numberDecline,
        bool isActive = true)
    {
        ApprovalLevelCode = approvalLevelCode;
        Description       = description;
        NumberAppr        = numberAppr;
        NumberDecline     = numberDecline;
        IsActive          = isActive;
    }

    public void Create(string approvalLevelCode, string? description, int numberAppr, int numberDecline,
        List<ApprovalLevelLine> approvalLevelLines,
        bool isActive = true)
    {
        Id                = Guid.NewGuid().ToString();
        ApprovalLevelCode = approvalLevelCode;
        Description       = description;
        NumberAppr        = numberAppr;
        NumberDecline     = numberDecline;
        IsActive          = isActive;

        foreach (var levelLine in approvalLevelLines)
        {
            var approvalLevelLine = new ApprovalLevelLine(levelLine.FatherId, levelLine.ApprovalUserId);
            _approvalLevelLines.Add(approvalLevelLine);
        }
    }
}

public class ApprovalLevelLine : Entity<string>
{
    public int FatherId { get; private set; }
    public int ApprovalUserId { get; private set; }
    public User? Approver { get; }


    public ApprovalLevelLine(int fatherId, int approvalUserId)
    {
        Id             = Guid.NewGuid().ToString();
        FatherId       = fatherId;
        ApprovalUserId = approvalUserId;
    }
}