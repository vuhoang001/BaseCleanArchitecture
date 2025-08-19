namespace Application.Interfaces;

public interface IOrderRepository
{
    Task<bool>                   CreateAsync(Domain.Entities.Order order);
    Task<Domain.Entities.Order?> FindByIdAsync(string id);
    
    Task<bool> UpdateAsync(Domain.Entities.Order order);
    
    
}