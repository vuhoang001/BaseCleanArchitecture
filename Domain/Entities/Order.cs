using Domain.Enums;
using Domain.Events.Order;

namespace Domain.Entities;

public class Order : Aggregate<string>, IApprovableEntity
{
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public decimal TotalPrice => _orderItems.Sum(x => x.UnitPrice * x.Quantity);

    private readonly List<OrderItem> _orderItems = new();

    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    private Order()
    {
    }

    public Order(string code, string name,
        IEnumerable<(string itemCode, string itemName, decimal quantity, decimal unitPrice)> items)
    {
        Id   = Guid.NewGuid().ToString();
        Code = code;
        Name = name;

        foreach (var item in items)
        {
            AddItem(item.itemCode, item.itemName, item.quantity, item.unitPrice);
        }
    }

    public void Add(string code, string name,
        IEnumerable<(string itemCode, string itemName, decimal quantity, decimal unitPrice)> items)
    {
        Id   = Guid.NewGuid().ToString();
        Code = code;
        Name = name;

        foreach (var item in items)
        {
            AddItem(item.itemCode, item.itemName, item.quantity, item.unitPrice);
        }
    }

    public void Update(string name,
        IEnumerable<(string itemCode, string itemName, decimal quantity, decimal unitPrice)> items)
    {
        Name = name;

        var orderItems = items.ToList();

        var newProductsCodes = orderItems.Select(x => x.itemCode).ToList();
        _orderItems.RemoveAll(x => !newProductsCodes.Contains(x.ProductCode));

        foreach (var item in orderItems)
        {
            var existingItem = _orderItems.FirstOrDefault(x => x.ProductCode == item.itemCode);
            if (existingItem != null)
            {
                existingItem.UpdateItem(item.quantity, item.unitPrice);
            }
            else
            {
                AddItem(item.itemCode, item.itemName, item.quantity, item.unitPrice);
            }
        }
    }

    public void AddItem(string productCode, string productName, decimal quanitty, decimal unitPrice)
    {
        var orderItem = new OrderItem(Id, productCode, productName, quanitty, unitPrice);
        _orderItems.Add(orderItem);
    }

    public string EntityId { get; set; }
    public string EntityType { get; set; }
    public ApprovalStatus ApprStatus { get; set; }

    public void ApplyApproval()
    {
        throw new NotImplementedException();
    }

    public void ApplyRejection(string reason)
    {
        throw new NotImplementedException();
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

    public void UpdateItem(decimal quantity, decimal unitPrice)
    {
        Quantity  = quantity;
        UnitPrice = unitPrice;
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