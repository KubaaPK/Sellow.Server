using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Sellow.Modules.EmailSending.Core.EmailClients;

internal sealed class SendgridClient
{
    private readonly ILogger<SendgridClient> _logger;
    private readonly ISendGridClient _sendGridClient;

    public SendgridClient(ILogger<SendgridClient> logger, IConfiguration configuration)
    {
        _logger = logger;
        _sendGridClient = new SendGridClient(configuration["Sendgrid:ApiKey"]);
    }

    public async Task SendEmail(SendGridMessage email)
    {
        await _sendGridClient.SendEmailAsync(email);
        _logger.LogInformation("Email '{Email}' has been sent", email.Serialize());
    }
}