namespace Application.Dtos;

public record CreateOrderRequest
{
    /// <summary>Mã đơn hàng</summary>
    public string Code { get; init; } = "";

    /// <summary>Tên đơn hàng</summary>
    public string Name { get; init; } = "";

    /// <summary>Tổng giá trị đơn hàng</summary>
    public decimal TotalPrice { get; init; }

    /// <summary>Danh sách sản phẩm trong đơn hàng</summary>
    public List<OrderItemDto> OrderItems { get; init; } = [];
}

public record OrderItemDto
{
    /// <summary>Mã sản phẩm</summary>
    public string ProductCode { get; init; } = "";

    /// <summary>Tên sản phẩm</summary>
    public string ProductName { get; init; } = "";

    /// <summary>Số lượng</summary>
    public decimal Quantity { get; init; }

    /// <summary>Đơn giá</summary>
    public decimal UnitPrice { get; init; }
}