using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Services.Order;

public class OrderRepos : Repository<Domain.Entities.Order, int>, IOrderRepository
{
    public OrderRepos(ApplicationDbContext context) : base(context)
    {
    }
}