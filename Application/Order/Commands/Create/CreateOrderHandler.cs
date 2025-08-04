using Application.Data;

namespace Application.Order.Commands.Create;

public class CreateOrderHandler(IApplicationDbContext context) : ICommandHandler<CreateOrderCommand, bool>
{
    public async Task<bool> Handle(CreateOrderCommand handle, CancellationToken cancellationToken)
    {
        var order = new Domain.Entities.Order(handle.Order.Code, handle.Order.Name);
        handle.Order.OrderItems.ForEach(x => order.AddItem(x.ProductCode, x.ProductName, x.Quantity, x.UnitPrice));

        context.Orders.Add(order);

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}