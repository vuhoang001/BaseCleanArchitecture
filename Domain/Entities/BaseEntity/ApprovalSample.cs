namespace Domain.Entities.BaseEntity;

public class ApprovalSample : Aggregate<string>
{
    public string ApprovalSampleName { get; set; } = string.Empty;
    public string ApprovalSampleCode { get; set; } = string.Empty;
    public string ApprovalSampleDescription { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    
}