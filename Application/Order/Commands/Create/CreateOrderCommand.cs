namespace Application.Order.Commands.Create;

public record CreateOrderCommand(Domain.Entities.Order Order) : ICommand<bool>;