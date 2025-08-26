using Domain.Enums;

namespace Domain.Entities;

public class ApprovalTemplate : Aggregate<string>
{
    public string ApprovalTemplateCode { get; private set; } = null!;
    public string ApprovalTemplateName { get; private set; } = null!;

    public bool IsCondition { get; private set; }
    public bool IsActive { get; private set; }

    private readonly List<ApprovalTemplateCreator>   _approvalTemplateCreators   = new();
    private readonly List<ApprovalTemplateCondition> _approvalTemplateConditions = new();
    private readonly List<ApprovalTemplateProcess>   _approvalTemplateProcesses  = new();
    private readonly List<ApprovalTemplateDocument>  _approvalTemplateDocuments  = new();

    public IReadOnlyList<ApprovalTemplateCreator> ApprovalTemplateCreators => _approvalTemplateCreators.AsReadOnly();

    public IReadOnlyList<ApprovalTemplateCondition> ApprovalTemplateConditions =>
        _approvalTemplateConditions.AsReadOnly();

    public IReadOnlyList<ApprovalTemplateProcess> ApprovalTemplateProcesses => _approvalTemplateProcesses.AsReadOnly();
    public IReadOnlyList<ApprovalTemplateDocument> ApprovalTemplateDocuments => _approvalTemplateDocuments.AsReadOnly();


    public ApprovalTemplate()
    {
    }

    public ApprovalTemplate(string approvalTemplateCode, string approvalTemplateName, bool isCondition, bool isActive)
    {
        Id                   = Guid.NewGuid().ToString();
        ApprovalTemplateCode = approvalTemplateCode;
        ApprovalTemplateName = approvalTemplateName;
        IsCondition          = isCondition;
        IsActive             = isActive;
    }

    public void Add(string approvalTemplateCode, string approvalTemplateName, bool isCondition, bool isActive,
        IEnumerable<int>? approvalTemplateCreatorIds,
        IEnumerable<DocType>? approvalTemplateDocumentTypes,
        IEnumerable<string>? approvalTemplateProcessIds
    )
    {
        Id                   = Guid.NewGuid().ToString();
        ApprovalTemplateCode = approvalTemplateCode;
        ApprovalTemplateName = approvalTemplateName;
        IsCondition          = isCondition;
        IsActive             = isActive;

        if (approvalTemplateCreatorIds != null)
            foreach (var approvalTemplateCreatorId in approvalTemplateCreatorIds)
            {
                var newApprLevelTemplateCreator = new ApprovalTemplateCreator(Id, approvalTemplateCreatorId);
                _approvalTemplateCreators.Add(newApprLevelTemplateCreator);
            }

        if (approvalTemplateDocumentTypes != null)
            foreach (var approvalTemplateDocumentType in approvalTemplateDocumentTypes)
            {
                var newApprovalTemplateDocument = new ApprovalTemplateDocument(Id, approvalTemplateDocumentType);
                _approvalTemplateDocuments.Add(newApprovalTemplateDocument);
            }

        if (approvalTemplateProcessIds != null)
            foreach (var approvalTemplateProcessId in approvalTemplateProcessIds)
            {
                var newApprovalTemplateProcess = new ApprovalTemplateProcess(Id, approvalTemplateProcessId);
                _approvalTemplateProcesses.Add(newApprovalTemplateProcess);
            }
    }
}

public class ApprovalTemplateCreator : Entity<string>
{
    public string FatherId { get; private set; } = null!;
    public int CreatorId { get; private set; }
    public User? Creator { get; private set; }

    public ApprovalTemplateCreator()
    {
    }

    public ApprovalTemplateCreator(string fatherId, int creatorId)
    {
        Id        = Guid.NewGuid().ToString();
        FatherId  = fatherId;
        CreatorId = creatorId;
    }
}

public class ApprovalTemplateDocument : Entity<string>
{
    public string FatherId { get; private set; } = null!;

    public DocType DocType { get; private set; }

    public ApprovalTemplateDocument()
    {
    }

    public ApprovalTemplateDocument(string fatherId, DocType docType)
    {
        Id       = Guid.NewGuid().ToString();
        FatherId = fatherId;
        DocType  = docType;
    }
}

public class ApprovalTemplateProcess : Entity<string>
{
    public string FatherId { get; private set; } = null!;

    public string ApporvalLevelId { get; private set; } = null!;
    public ApprovalLevel? ApprovalLevel { get; private set; }

    public ApprovalTemplateProcess()
    {
    }

    public ApprovalTemplateProcess(string fatherId, string apporvalLevelId)
    {
        Id              = Guid.NewGuid().ToString();
        FatherId        = fatherId;
        ApporvalLevelId = apporvalLevelId;
    }
}

public class ApprovalTemplateCondition : Entity<string>
{
    public string FatherId { get; private set; } = null!;

    public ApprovalTemplateCondition()
    {
    }

    public ApprovalTemplateCondition(string fatherId)
    {
        Id       = Guid.NewGuid().ToString();
        FatherId = fatherId;
    }
}