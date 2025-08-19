using Application.Interfaces;
using Domain.Events.Order;
using Shared.ExceptionBase;

namespace Application.Order.Commands.Create;

public class CreateOrderHandler(IOrderRepository orderRepository)
    : ICommandHandler<CreateOrderCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(CreateOrderCommand handle, CancellationToken cancellationToken)
    {
        var orderItems = handle.Order.OrderItems.Select(x => (x.ProductCode, x.ProductName, x.Quantity, x.UnitPrice));
        var order      = new Domain.Entities.Order(handle.Order.Code, handle.Order.Name, orderItems);
        order.AddDomainEvent(new OrderCreateProductEvent(order));

        await orderRepository.CreateAsync(order);

        return Result<bool>.Success(true);
    }
}