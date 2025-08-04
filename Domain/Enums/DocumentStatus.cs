namespace Domain.Enums;

/// <summary>
/// Trạng thái chứng từ
/// </summary>
public enum DocumentStatus
{
    /// <summary>
    /// Nháp
    /// </summary>
    Draft,

    /// <summary>
    /// Mở
    /// </summary>
    Open,

    /// <summary>
    /// Đang thực hiện
    /// </summary>
    Processing,

    /// <summary>
    /// Đóng
    /// </summary>
    Close,

    /// <summary>
    /// Hủy
    /// </summary>
    Cancelled,
}