using Application.Interfaces;
using Application.Order.Commands.Update;
using Shared.ExceptionBase;

namespace Application.Handlers.Order.Commands.Update;

public class UpdateOrderHandler(IOrderRepository orderRepos) : ICommandHandler<UpdateOrderCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepos.FindByIdAsync(request.Id);
        if (order is null) return Result<bool>.Failure($"Không tìm thấy đơn hàng {request.Id}");

        var orderItems = request.Order.OrderItems
            .Select(x => (x.ProductCode, x.ProductName, x.Quantity, x.UnitPrice));
        order.Update(request.Order.Name, orderItems);

        await orderRepos.UpdateAsync(order);
        return Result<bool>.Success(true);
    }
}