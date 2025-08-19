namespace Domain.Entities;

public class PurchasePlan : Aggregate<string>, IDocBase
{
    public string DocCode { get; private set; }
    public string DocName { get; private set; }
    public DateTime DocDate { get; private set; }

    public decimal TotalPrice => _itemLines.Sum(x => x.UnitPrice * x.Quantity);

    public readonly List<PurchasePlanLine> _itemLines = new();

    public IReadOnlyList<PurchasePlanLine> ItemLines => _itemLines.AsReadOnly();

    public PurchasePlan()
    {
    }

    public PurchasePlan(string docCode, string docName, DateTime docDate,
        IEnumerable<(string itemCode, string itemName, decimal unitPrice, decimal quantity)> itemLines)
    {
        Id      = Guid.NewGuid().ToString();
        DocCode = docCode;
        DocName = docName;
        DocDate = docDate;

        foreach (var item in itemLines)
        {
            var newItem = new PurchasePlanLine(Id, item.itemCode, item.itemName, item.unitPrice, item.quantity);
            _itemLines.Add(newItem);
        }
    }
}

public class PurchasePlanLine : Entity<string>, IDocLineBase
{
    public string FatherId { get; private set; }
    public string ItemCode { get; private set; } = null!;
    public string ItemName { get; private set; } = null!;
    public decimal UnitPrice { get; private set; }
    public decimal Quantity { get; private set; }

    public PurchasePlanLine()
    {
    }

    public PurchasePlanLine(string id, string itemCode, string itemName, decimal unitPrice, decimal quantity)
    {
        FatherId  = id;
        ItemCode  = itemCode;
        ItemName  = itemName;
        UnitPrice = unitPrice;
        Quantity  = quantity;
    }
}