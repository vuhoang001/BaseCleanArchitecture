namespace Domain.Enums;

/// <summary>
/// Loại chứng từ
/// </summary>
public enum DocType
{
    /// <summary>
    /// Yêu cầu mua sắm
    /// </summary>
    PurchaseRequest,

    /// <summary>
    /// Đơn hàng mua sắm
    /// </summary>
    PurchaseOrder,

    /// <summary>
    /// Tổng hợp mua sắm
    /// </summary>
    PurchaseSummary,

    /// <summary>
    /// Kế hoạch mua sắm
    /// </summary>
    PurchasePlan,

    /// <summary>
    /// Nhập kho
    /// </summary>
    WarehouseReceipt
}