namespace Domain.Events.Order;

public record OrderCreateProductEvent(Entities.Order Order) : IDomainEvent;