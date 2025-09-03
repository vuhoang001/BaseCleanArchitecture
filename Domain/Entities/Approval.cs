using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Entities;

public class Approval : Aggregate<int>
{
    public int EntityId { get; private set; } = default!;
    public string EntityType { get; private set; } = default!;

    public string ApprovalLevelId { get; private set; } = default!;

    public string ApprovalTemplateId { get; private set; } = default!;

    public ApprovalLevel? ApprovalLevel { get; private set; } = default!;

    public ApprovalTemplate? ApprovalTemplate { get; private set; }

    public ApprovalStatus ApprStatus { get; private set; } = ApprovalStatus.Pending;

    public static Approval Create(int entityId, string entityType)
    {
        var approval = new Approval
        {
            EntityId   = entityId,
            EntityType = entityType,
            ApprStatus = ApprovalStatus.Pending
        };
        return approval;
    }
}

public class ApprovalLine : Entity<int>
{
    public int FatherId { get; private set; } = default!;

    [JsonIgnore] [NotMapped] public Approval Approval { get; private set; } = default!;

    public string Status { get; private set; } = "W";
}