using Application.Data;
using Application.Interfaces;
using Shared.ExceptionBase;

namespace Application.Handlers.Order.Queries.GetById;

public class GetOrderByIdHandler(IApplicationDbContext context, IOrderRepository orderRepository)
    : IQueryHandler<GetOrderByIdCommand, Result<Domain.Entities.Order>>
{
    public async Task<Result<Domain.Entities.Order>> Handle(GetOrderByIdCommand request,
        CancellationToken cancellationToken)
    {
        var order = await orderRepository.FindByIdAsync(request.Id);
        if (order is null) throw new Exception();
        return Result<Domain.Entities.Order>.Success(order);
    }
}