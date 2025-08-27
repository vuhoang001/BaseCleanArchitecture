using Application.Dtos;
using Shared.ExceptionBase;
using Shared.Extensions;

namespace Application.Handlers.Order.Commands.Create;

// [Permission("order.create")]
public record CreateOrderCommand(CreateOrderRequest Order) : ICommand<Result<bool>>;