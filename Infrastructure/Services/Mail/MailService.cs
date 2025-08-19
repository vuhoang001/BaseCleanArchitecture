using System.Net.Mail;
using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Shared.ExceptionBase;

namespace Infrastructure.Services.Mail;

public class MailService(IOptions<EmailSettings> emailSettings, IWebHostEnvironment env) : IMailService
{
    public async Task SendMailAsync(EmailRequest dto)
    {
        try
        {
            using var client       = CreateSmtpClient();
            using var mailSettings = CreateMailMessage(dto);

            await client.SendMailAsync(mailSettings);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task SendMailBulkAsync(IEnumerable<EmailRequest> dtos)
    {
        foreach (var emailRequest in dtos)
        {
            await SendMailAsync(emailRequest);
            await Task.Delay(1000);
        }
    }

    private SmtpClient CreateSmtpClient()
    {
        return new SmtpClient(emailSettings.Value.SmtpHost, emailSettings.Value.SmtpPort)
        {
            Credentials = new System.Net.NetworkCredential(emailSettings.Value.Username, emailSettings.Value.Password),
            EnableSsl   = emailSettings.Value.EnableSsl,
        };
    }

    private MailMessage CreateMailMessage(EmailRequest request)
    {
        if (request.IsHtml) request.Body = GetEmailTemplate(request);

        var mailMessage = new MailMessage
        {
            From       = new MailAddress(emailSettings.Value.FromEmail, emailSettings.Value.FromName),
            Subject    = request.Subject,
            Body       = request.Body,
            IsBodyHtml = request.IsHtml
        };

        mailMessage.To.Add(request.To);

        return mailMessage;
    }

    private string GetEmailTemplate(EmailRequest request)
    {
        if (request.IsHtml is false) throw new ApiBadRequestException("Xử lý html không hợp lệ");

        var templatePath = Path.Combine(env.WebRootPath, "Templates", request.Body);

        if (!File.Exists(templatePath))
            throw new ApiBadRequestException("Xử lý html không hợp lệ");

        var emailTemplate = File.ReadAllText(templatePath);

        emailTemplate = emailTemplate.Replace("{{Name}}", request.To);
        if (request.DynamicParameters != null)
            foreach (var param in request.DynamicParameters)
            {
                emailTemplate = emailTemplate.Replace($"{{{{{param.Key}}}}}", param.Value);
            }

        return emailTemplate;
    }
}