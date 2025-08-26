using Application.Dtos;
using Shared.ExceptionBase;

namespace Application.Handlers.Order.Commands.Create;

public record CreateOrderCommand(CreateOrderRequest Order) : ICommand<Result<bool>>;