using Application.Dtos;
using Application.PurchasePlan.Commands.Create;

namespace Application.Interfaces;

public interface IMailService
{
    Task SendMailAsync(EmailRequest dto);
    Task SendMailBulkAsync(IEnumerable<EmailRequest> dtos);
}