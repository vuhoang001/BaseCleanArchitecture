using Application.Dtos;
using Application.Interfaces;
using Domain.Events.Order;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.Order.EventHandlers;

public class OrderCreateEventHandler(ILogger<OrderCreateEventHandler> logger, IMailService mailService)
    : INotificationHandler<OrderCreateProductEvent>
{
    public Task Handle(OrderCreateProductEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain event handler: {DomainEvent}", notification.GetType().Name);
        _ = Task.Run(async () =>
        {
            await mailService.SendMailAsync(new EmailRequest
            {
                To                = "vuhoang250203@gmail.com",
                Subject           = "Toi ten la hoang",
                Body              = "kekek",
                IsHtml            = false,
                DynamicParameters = null
            });
        });
        return Task.CompletedTask;
    }
}