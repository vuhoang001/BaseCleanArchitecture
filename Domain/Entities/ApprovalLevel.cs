using Domain.Enums;

namespace Domain.Entities;

public class ApprovalLevel : Aggregate<string>
{
    public string ApprovalLevelCode { get; private set; } = default!;
    public string ApprovalLevelName { get; private set; } = default!;
    public bool IsActive { get; private set; }

    public int NumberOfApproval { get; private set; }
    public int NumberOfRejection { get; private set; }

    public IReadOnlyList<ApprovalLevelUserAppr> ApprovalLevelUserApprs => _approvalLevelUserApprs.AsReadOnly();
    private readonly List<ApprovalLevelUserAppr> _approvalLevelUserApprs = new();

    public ApprovalLevel()
    {
    }

    public void Update(string approvalLevelName, bool isActive, int numberOfApproval, int numberOfRejection)
    {
        ApprovalLevelName = approvalLevelName;
        IsActive          = isActive;
        NumberOfApproval  = numberOfApproval;
        NumberOfRejection = numberOfRejection;
    }

    public void UpdateUser(IEnumerable<int> newUserIds)
    {
        var newUserIdsList  = newUserIds.ToList();
        var existingUserIds = _approvalLevelUserApprs.Select(x => x.UserId).ToList();

        var usersToRemove = _approvalLevelUserApprs.Where(x => !newUserIdsList.Contains(x.UserId)).ToList();
        usersToRemove.ForEach(x => _approvalLevelUserApprs.Remove(x));

        var userToAdd = newUserIdsList.Where(userId => !existingUserIds.Contains(userId)).ToList();
        userToAdd.ForEach(x =>
        {
            var newUser = new ApprovalLevelUserAppr(Id, x);
            _approvalLevelUserApprs.Add(newUser);
        });
    }

    public ApprovalLevel(string approvalLevelCode, string approvalLevelName, bool isActive, int numberOfApproval,
        int numberOfRejection)
    {
        Id                = Guid.NewGuid().ToString();
        ApprovalLevelCode = approvalLevelCode;
        ApprovalLevelName = approvalLevelName;
        IsActive          = isActive;
        NumberOfApproval  = numberOfApproval;
        NumberOfRejection = numberOfRejection;
    }

    public void AddItem(IEnumerable<int> approvalLevelUserApprs)
    {
        foreach (var user in approvalLevelUserApprs)
        {
            var newUser = new ApprovalLevelUserAppr(Id, user);
            _approvalLevelUserApprs.Add(newUser);
        }
    }
}

public class ApprovalLevelUserAppr : Entity<string>
{
    public string FatherId { get; private set; } = null!;
    public int UserId { get; private set; }
    public User? User { get; private set; }

    public ApprovalLevelUserAppr()
    {
    }

    public ApprovalLevelUserAppr(string fatherId, int userId)
    {
        Id       = Guid.NewGuid().ToString();
        FatherId = fatherId;
        UserId   = userId;
    }
}