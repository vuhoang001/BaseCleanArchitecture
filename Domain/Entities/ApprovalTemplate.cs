using Domain.Enums;

namespace Domain.Entities;

public class ApprovalTemplate : Aggregate<int>
{
    public string ApprovalTemplateCode { get; private set; } = null!;
    public string ApprovalTemplateName { get; private set; } = null!;

    public bool IsCondition { get; private set; }
    public bool IsActive { get; private set; }

    private readonly List<ApprovalTemplateCreator>  _approvalTemplateCreators  = new();
    private readonly List<ApprovalTemplateProcess>  _approvalTemplateProcesses = new();
    private readonly List<ApprovalTemplateDocument> _approvalTemplateDocuments = new();

    public IReadOnlyList<ApprovalTemplateCreator> ApprovalTemplateCreators => _approvalTemplateCreators.AsReadOnly();


    public IReadOnlyList<ApprovalTemplateProcess> ApprovalTemplateProcesses => _approvalTemplateProcesses.AsReadOnly();
    public IReadOnlyList<ApprovalTemplateDocument> ApprovalTemplateDocuments => _approvalTemplateDocuments.AsReadOnly();


    public ApprovalTemplate()
    {
    }

    public ApprovalTemplate(string approvalTemplateCode, string approvalTemplateName, bool isCondition, bool isActive)
    {
        ApprovalTemplateCode = approvalTemplateCode;
        ApprovalTemplateName = approvalTemplateName;
        IsCondition          = isCondition;
        IsActive             = isActive;
    }

    public void Add(string approvalTemplateCode, string approvalTemplateName, bool isCondition, bool isActive,
        IEnumerable<int>? approvalTemplateCreatorIds,
        IEnumerable<DocType>? approvalTemplateDocumentTypes,
        IEnumerable<int>? approvalTemplateProcessIds
    )
    {
        ApprovalTemplateCode = approvalTemplateCode;
        ApprovalTemplateName = approvalTemplateName;
        IsCondition          = isCondition;
        IsActive             = isActive;

        if (approvalTemplateCreatorIds != null)
            foreach (var approvalTemplateCreatorId in approvalTemplateCreatorIds)
            {
                var newApprLevelTemplateCreator = new ApprovalTemplateCreator(approvalTemplateCreatorId);
                _approvalTemplateCreators.Add(newApprLevelTemplateCreator);
            }

        if (approvalTemplateDocumentTypes != null)
            foreach (var approvalTemplateDocumentType in approvalTemplateDocumentTypes)
            {
                var newApprovalTemplateDocument = new ApprovalTemplateDocument(approvalTemplateDocumentType);
                _approvalTemplateDocuments.Add(newApprovalTemplateDocument);
            }

        if (approvalTemplateProcessIds != null)
            foreach (var approvalTemplateProcessId in approvalTemplateProcessIds)
            {
                var newApprovalTemplateProcess = new ApprovalTemplateProcess(approvalTemplateProcessId);
                _approvalTemplateProcesses.Add(newApprovalTemplateProcess);
            }
    }
}

public class ApprovalTemplateCreator : Entity<int>
{
    public int FatherId { get; private set; }
    public int CreatorId { get; private set; }
    public User? Creator { get; private set; }

    public ApprovalTemplateCreator()
    {
    }

    public ApprovalTemplateCreator(int creatorId)
    {
        CreatorId = creatorId;
    }
}

public class ApprovalTemplateDocument : Entity<int>
{
    public int FatherId { get; private set; }

    public DocType DocType { get; private set; }

    public ApprovalTemplateDocument()
    {
    }

    public ApprovalTemplateDocument(DocType docType)
    {
        DocType = docType;
    }
}

public class ApprovalTemplateProcess : Entity<int>
{
    public int FatherId { get; private set; }

    public int ApporvalLevelId { get; private set; }
    public ApprovalLevel? ApprovalLevel { get; private set; }

    public ApprovalTemplateProcess()
    {
    }

    public ApprovalTemplateProcess(int apporvalLevelId)
    {
        ApporvalLevelId = apporvalLevelId;
    }
}