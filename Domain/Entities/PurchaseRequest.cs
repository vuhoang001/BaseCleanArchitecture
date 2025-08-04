namespace Domain.Entities;

public class PurchaseRequest : Aggregate<string>
{
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public decimal TotalPrice => _purchaseRequestItems.Sum(x => x.UnitPrice * x.Quantity);

    private readonly List<PurchaseRequestItems> _purchaseRequestItems = new();
    public IReadOnlyList<PurchaseRequestItems> PurchaseRequestItems => _purchaseRequestItems.AsReadOnly();

    private PurchaseRequest()
    {
    }

    public PurchaseRequest(string code, string name)
    {
        Id   = Guid.NewGuid().ToString();
        Code = code;
        Name = name;
    }

    public void AddItem(string code, string name, decimal quantity, decimal price)
    {
        var item = new PurchaseRequestItems(Id, code, name, quantity, price);
        _purchaseRequestItems.Add(item);
    }

    public void RemoveItem(string code)
    {
        var item = _purchaseRequestItems.FirstOrDefault(x => x.ProductCode == code);
        if (item is not null)
            _purchaseRequestItems.Remove(item);
    }
}

public class PurchaseRequestItems : Entity<string>
{
    public string FatherId { get; private set; } = null!;
    public string ProductCode { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    protected PurchaseRequestItems()
    {
    }

    public PurchaseRequestItems(string id, string productCode, string productName, decimal quantity, decimal unitPrice)
    {
        FatherId    = id;
        Id          = Guid.NewGuid().ToString();
        ProductCode = productCode;
        ProductName = productName;
        Quantity    = quantity;
        UnitPrice   = unitPrice;
    }
}