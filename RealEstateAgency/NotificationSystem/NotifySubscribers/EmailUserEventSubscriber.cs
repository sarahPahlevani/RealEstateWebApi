using Coravel.Events.Interfaces;
using Coravel.Mailer.Mail.Interfaces;
using Microsoft.Extensions.Options;
using RealEstateAgency.Mailables;
using RealEstateAgency.Shared.BaseModels;
using System.Threading.Tasks;

namespace RealEstateAgency.NotificationSystem.NotifySubscribers
{
    public class EmailUserEventSubscriber : IListener<NotifyUserEvent>
    {
        private readonly IMailer _mailer;
        private readonly AppSetting _optionsValue;

        public EmailUserEventSubscriber(IMailer mailer, IOptions<AppSetting> options)
        {
            _mailer = mailer;
            _optionsValue = options.Value;
        }

        public async Task HandleAsync(NotifyUserEvent broadcast)
            => await _mailer.SendAsync(
                new UserEventEmail(broadcast.Message, _optionsValue.AdminEmailAddress));
    }
}
