using Domain.Events.Order;

namespace Domain.Entities;

public class Order : Aggregate<string>
{
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public decimal TotalPrice => OrderItems.Sum(x => x.UnitPrice * x.Quantity);

    public readonly List<OrderItem> OrderItems = new();

    public IReadOnlyList<OrderItem> OrderItemsList => OrderItems;


    public void Create(string code, string name, List<OrderItem> orderItems)
    {
        Code = code;
        Name = name;

        AddDomainEvent(new OrderCreateProductEvent(this));
    }

    public void AddItem(string productCode, string productName, decimal quanitty, decimal unitPrice)
    {
        var orderItem = new OrderItem(Id, productCode, productName, quanitty, unitPrice);
        OrderItems.Add(orderItem);
    }
}

public class OrderItem : Entity<string>
{
    public string FatherId { get; private set; }
    public string ProductCode { get; private set; }
    public string ProductName { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    public OrderItem(string fatherId, string productCode, string productName, decimal quantity, decimal unitPrice)
    {
        FatherId    = fatherId;
        ProductCode = productCode;
        ProductName = productName;
        Quantity    = quantity;
        UnitPrice   = unitPrice;
    }
}