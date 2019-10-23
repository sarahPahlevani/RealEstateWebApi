using Coravel.Mailer.Mail;
using RealEstateAgency.Controllers.Other;

namespace RealEstateAgency.Mailables
{
    public class VerifyEmailPage : Mailable<PublicController.VerifyEmailDto>
    {
        private readonly PublicController.VerifyEmailDto _model;

        public VerifyEmailPage(PublicController.VerifyEmailDto message)
        {
            _model = message;
        }

        public override void Build() =>
            To("miladbonak@gmail.com")
                .View("~/Views/Pages/VerifyEmail.cshtml", _model);
    }
}
