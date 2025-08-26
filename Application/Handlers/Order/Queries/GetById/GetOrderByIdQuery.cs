namespace Application.Order.Queries.GetById;

public record GetOrderByIdQuery(string Id) : IQuery<Domain.Entities.Order>;