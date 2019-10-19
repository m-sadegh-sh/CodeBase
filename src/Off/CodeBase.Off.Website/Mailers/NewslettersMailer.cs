namespace CodeBase.Off.Website.Mailers {
    using System.Net.Mail;

    using CodeBase.Off.Core.Domain;

    public class NewslettersMailer : CustomMailerBase {
        public virtual MailMessage PrepareSubscribConfirmation(Subscription model) {
            var from = new MailAddress(CurrentConfig.Mails.NoReply.Email,
                                       CurrentConfig.Mails.NoReply.DisplayName);

            var to = new MailAddress(model.Email);

            var message = new MailMessage(from,
                                          to) {
                                                  Subject = "تائید عضویتت در خبرنامه کدبیس",
                                                  IsBodyHtml = true
                                          };

            ViewData.Model = model;

            PopulateBody(message,
                         "PrepareSubscribConfirmation");

            return message;
        }
    }
}