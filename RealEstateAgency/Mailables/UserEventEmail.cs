using Coravel.Mailer.Mail;
using RealEstateAgency.NotificationSystem.NotifyMessages;

namespace RealEstateAgency.Mailables
{
    public class UserEventEmail : Mailable<UserNotifyMessage>
    {
        private readonly UserNotifyMessage _model;
        private readonly string _from;

        public UserEventEmail(UserNotifyMessage message, string from)
        {
            _model = message;
            _from = from;
        }

        public override void Build() =>
            To(_model.UserEmail).From(_from)
                .View("~/Views/Mail/UserEvent.cshtml", _model);
    }
}
