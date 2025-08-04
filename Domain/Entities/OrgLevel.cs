namespace Domain.Entities;

public class OrgLevel : Entity<string>
{
    public string OrgLevelName { get; private set; } = string.Empty;
    public string OrgLevelDescription { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    public OrgLevel()
    {
    }

    public OrgLevel(string orgLevelName, string orgLevelDescription, bool isActive)
    {
        Id                  = Guid.NewGuid().ToString();
        IsActive            = isActive;
        OrgLevelName        = orgLevelName;
        OrgLevelDescription = orgLevelDescription;
    }
}