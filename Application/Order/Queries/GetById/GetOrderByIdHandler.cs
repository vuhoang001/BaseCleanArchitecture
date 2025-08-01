using Application.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Order.Queries.GetById;

public class GetOrderByIdHandler(IApplicationDbContext context)
    : IQueryHandler<GetOrderByIdQuery, Domain.Entities.Order>
{
    public async Task<Domain.Entities.Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await context.Orders.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (order is null) throw new Exception();
        return order;
    }
}