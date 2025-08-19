namespace Application.Dtos;

public record OrgLevelCreate
{
    public string OrgLevelName { get; set; } = string.Empty;
    public string OrgLevelDescription { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}