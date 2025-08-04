using Application.Dtos;

namespace Application.Order.Commands.Create;

public record CreateOrderCommand(CreateOrderDto Order) : ICommand<bool>;