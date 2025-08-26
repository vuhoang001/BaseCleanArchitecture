using Application.Interfaces;
using Domain.Events.Order;
using Shared.ExceptionBase;

namespace Application.Handlers.Order.Commands.Create;

public class CreateOrderHandler(IOrderRepository orderRepository, ICodeGeneration codeGeneration)
    : ICommandHandler<CreateOrderCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(CreateOrderCommand handle, CancellationToken cancellationToken)
    {
        var code       = await codeGeneration.GenerateCodeAsync<Domain.Entities.Order>(x => x.Code, "Order");
        var orderItems = handle.Order.OrderItems.Select(x => (x.ProductCode, x.ProductName, x.Quantity, x.UnitPrice));
        var order      = new Domain.Entities.Order(code, handle.Order.Name, orderItems);
        order.AddDomainEvent(new OrderCreateProductEvent(order));

        await orderRepository.CreateAsync(order);

        return Result<bool>.Success(true);
    }
}