using Shared.ExceptionBase;

namespace Application.PurchasePlan.Commands.Create;

public record CreatePurchasePlanCommand(CreatePurchasePlanDto Dto) : ICommand<Result<bool>>;

public record CreatePurchasePlanDto
{
    public string DocCode { get; set; } = "";
    public string DocName { get; set; } = "";
    public DateTime DocDate { get; set; }

    public List<CreatePurchasePlanLineDto> ItemLines { get; set; } = new();
}

public record CreatePurchasePlanLineDto
{
    public string ItemCode { get; set; } = "";
    public string ItemName { get; set; } = "";
    public decimal UnitPrice { get; set; }
    public decimal Quantity { get; set; }
}