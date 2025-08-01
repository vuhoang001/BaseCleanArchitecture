using Application.Data;

namespace Application.Order.Commands.Create;

public class CreateOrderHandler(IApplicationDbContext context) : ICommandHandler<CreateOrderCommand, bool>
{
    public async Task<bool> Handle(CreateOrderCommand handle, CancellationToken cancellationToken)
    {
        context.Orders.Add(handle.Order);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}