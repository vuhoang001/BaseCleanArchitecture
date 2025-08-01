namespace Application.Dtos;

public class CreateOrderDto
{
    public string Code { get; set; } = "";
    public string Name { get; set; } = "";
    public decimal TotalPrice { get; set; }
    public List<OrderItemDto> OrderItems { get; set; } = [];
}

public class OrderItemDto
{
    public string ProductCode { get; set; } = "";
    public string ProductName { get; set; } = "";
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}