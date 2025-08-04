namespace Domain.Enums;

/// <summary>
/// Trạng thái phê duyệt
/// </summary>
public enum ApprovalStatus
{
    /// <summary>
    /// Đang thực hiện
    /// </summary>
    Pending,

    /// <summary>
    /// Đã phê duyệt
    /// </summary>
    Approved,

    /// <summary>
    /// Hủy phê duyệt
    /// </summary>
    Cancelled,

    /// <summary>
    /// Từ chối phê duyệt
    /// </summary>
    Rejected
}