namespace Domain.Entities;

public class Order : Aggregate<string>
{
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public decimal TotalPrice => _orderItems.Sum(x => x.UnitPrice * x.Quantity);

    private readonly List<OrderItem> _orderItems = new();

    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    private Order()
    {
    }

    public Order(string code, string name)
    {
        Id   = Guid.NewGuid().ToString();
        Code = code;
        Name = name;
    }

    public void AddItem(string productCode, string productName, decimal quanitty, decimal unitPrice)
    {
        var orderItem = new OrderItem(Id, productCode, productName, quanitty, unitPrice);
        _orderItems.Add(orderItem);
    }
}

public class OrderItem : Entity<string>
{
    public string FatherId { get; private set; } = null!;
    public string ProductCode { get; private set; } = null!;
    public string ProductName { get; private set; } = null!;
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    private OrderItem()
    {
    }

    public OrderItem(string fatherId, string productCode, string productName, decimal quantity, decimal unitPrice)
    {
        Id          = Guid.NewGuid().ToString();
        FatherId    = fatherId;
        ProductCode = productCode;
        ProductName = productName;
        Quantity    = quantity;
        UnitPrice   = unitPrice;
    }
}