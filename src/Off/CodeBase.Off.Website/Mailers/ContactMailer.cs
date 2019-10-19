namespace CodeBase.Off.Website.Mailers {
    using System.Net.Mail;

    using CodeBase.Off.Website.Models;

    public class ContactMailer : CustomMailerBase {
        public virtual MailMessage PrepareUserMessage(ContactModel model) {
            var from = new MailAddress(CurrentConfig.Mails.NoReply.Email,
                                       CurrentConfig.Mails.NoReply.DisplayName);

            var to = new MailAddress(CurrentConfig.Mails.Contact.Email,
                                     CurrentConfig.Mails.Contact.DisplayName);

            var message = new MailMessage(from,
                                          to) {
                                                  Subject = string.Format("پیام جدید از {0} ({1})",
                                                                          model.SenderName,
                                                                          model.SenderEmail),
                                                  IsBodyHtml = true
                                          };

            ViewData.Model = model;

            PopulateBody(message,
                         "PrepareUserMessage");

            return message;
        }
    }
}