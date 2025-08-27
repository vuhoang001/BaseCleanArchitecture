using Shared.ExceptionBase;

namespace Application.Handlers.Order.Queries.GetById;

public record GetOrderByIdCommand(string Id) : IQuery<Result<Domain.Entities.Order>>;