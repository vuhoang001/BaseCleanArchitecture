using Application.Data;
using Application.Interfaces;
using Application.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Shared.ExceptionBase;

namespace Application.Handlers.Order.Queries.GetById;

public class GetOrderByIdHandler(
    IOrderRepository orderRepository,
    IApplicationDbContext applicationDbContext)
    : IQueryHandler<GetOrderByIdCommand, Result<Domain.Entities.Order>>
{
    public async Task<Result<Domain.Entities.Order>> Handle(GetOrderByIdCommand request,
        CancellationToken cancellationToken)
    {
        var order = await orderRepository.Query()
            .ToListAsync();
        var result = await applicationDbContext.OrderItem.ToListAsync();
        if (order is null) throw new Exception();
        return Result<Domain.Entities.Order>.Success(order.FirstOrDefault());
    }
}