using Shared.ExceptionBase;

namespace Application.Order.Commands.Update;

public record UpdateOrderCommand(string Id, UpdateOrderDto Order) : ICommand<Result<bool>>;

public record UpdateOrderDto
{
    /// <summary>
    /// Tên đơn hàng
    /// </summary>
    public string Name { get; init; } = "";

    /// <summary>Tổng giá trị đơn hàng</summary>
    public decimal TotalPrice { get; init; }

    /// <summary>Danh sách sản phẩm trong đơn hàng</summary>
    public List<UpdateOrderLineDto> OrderItems { get; init; } = [];
}

public record UpdateOrderLineDto
{
    public string? Id { get; set; }

    /// <summary>Mã sản phẩm</summary>
    public string ProductCode { get; init; } = "";

    /// <summary>Tên sản phẩm</summary>
    public string ProductName { get; init; } = "";

    /// <summary>Số lượng</summary>
    public decimal Quantity { get; init; }

    /// <summary>Đơn giá</summary>
    public decimal UnitPrice { get; init; }
}