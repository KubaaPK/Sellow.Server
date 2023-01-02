using MediatR;
using Microsoft.Extensions.Configuration;
using Sellow.Modules.Auth.IntegrationEvents;
using Sellow.Modules.EmailSending.Core.EmailClients;
using SendGrid.Helpers.Mail;

namespace Sellow.Modules.EmailSending.Core.Features;

internal sealed class SendUserActivationEmail : INotificationHandler<UserCreated>
{
    private readonly IConfiguration _configuration;
    private readonly SendgridClient _sendgridClient;

    public SendUserActivationEmail(IConfiguration configuration, SendgridClient sendgridClient)
    {
        _configuration = configuration;
        _sendgridClient = sendgridClient;
    }

    public async Task Handle(UserCreated notification, CancellationToken cancellationToken)
    {
        var email = CreateUserActivationEmail(notification);
        await _sendgridClient.SendEmail(email);
    }

    private SendGridMessage CreateUserActivationEmail(UserCreated notification) => MailHelper
        .CreateSingleTemplateEmail(
            new EmailAddress("sellow@sellow.io"),
            new EmailAddress(notification.Email),
            _configuration["Sendgrid:Templates:UserActivation"], new
            {
                notification.Username,
                ActivationLink = $"{_configuration["Auth:UserActivationUrl"]}/{notification.Id}"
            });
}