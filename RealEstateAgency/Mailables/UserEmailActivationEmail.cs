using Coravel.Mailer.Mail;
using RealEstateAgency.Dtos.ModelDtos.RBAC;

namespace RealEstateAgency.Mailables
{
    public class UserEmailActivationEmail : Mailable<UserAccountDto>
    {
        private readonly UserAccountDto _model;
        private readonly string _from;

        public UserEmailActivationEmail(UserAccountDto message, string emailActivationBaseUrl, string from)
        {
            _model = message;
            _from = from;
            if (!emailActivationBaseUrl.EndsWith("/"))
                emailActivationBaseUrl += "/";
            _model.ActivationLink = emailActivationBaseUrl + _model.ActivationKey;
        }

        public override void Build() =>
            To(_model.Email).From(_from)
                .View("~/Views/Mail/UserEmailActivation.cshtml", _model);
    }
}
