using Application.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Services.Order;

public class OrderRepos(ApplicationDbContext dbContext) : IOrderRepository
{
    public async Task<bool> CreateAsync(Domain.Entities.Order order)
    {
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Domain.Entities.Order?> FindByIdAsync(string id)
    {
        return await dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(Domain.Entities.Order order)
    {
        dbContext.Orders.Update(order);
        return await dbContext.SaveChangesAsync() > 0;
    }
}