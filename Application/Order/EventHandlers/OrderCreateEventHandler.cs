using Domain.Events.Order;
using Microsoft.Extensions.Logging;

namespace Application.Order.EventHandlers;

public class OrderCreateEventHandler(ILogger<OrderCreateEventHandler> logger)
    : INotificationHandler<OrderCreateProductEvent>
{
    public Task Handle(OrderCreateProductEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain event handler: {DomainEvent}", notification.GetType().Name);
        Console.WriteLine(notification.Order.Id);
        Console.WriteLine(notification.Order.Name);
        return Task.CompletedTask;
    }
}