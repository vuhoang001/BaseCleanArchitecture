using Shared.ExceptionBase;

namespace Application.Handlers.Order.Queries.GetById;

public record GetOrderByIdCommand(int Id) : IQuery<Result<Domain.Entities.Order>>;