namespace Application.Dtos;

public record PurchaseRequestDto
{
    /// <summary>
    /// Mã  
    /// </summary>
    public string Code { get; init; } = "";

    /// <summary>
    /// Tên
    /// </summary>
    public string Name { get; set; } = "";

    public List<PurchaseRequestItemsDto> PurchaseRequestItems { get; set; } = [];
}

public class PurchaseRequestItemsDto
{
    public string ProductCode { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}