namespace Domain.Entities;

public class OrgStructure : Entity<string>
{
    public string OrgStructureCode { get; set; } = string.Empty;
    public string OrgStructureDescription { get; set; } = string.Empty;
    public string OrgStrctureName { get; set; } = string.Empty;
    public int? ParentId { get; set; }
}