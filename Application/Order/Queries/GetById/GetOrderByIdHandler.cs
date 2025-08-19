using Application.Data;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Order.Queries.GetById;

public class GetOrderByIdHandler(IApplicationDbContext context, IOrderRepository orderRepository)
    : IQueryHandler<GetOrderByIdQuery, Domain.Entities.Order>
{
    public async Task<Domain.Entities.Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.FindByIdAsync(request.Id);
        if (order is null) throw new Exception();
        return order;
    }
}