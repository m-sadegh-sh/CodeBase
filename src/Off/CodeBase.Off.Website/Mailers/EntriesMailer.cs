namespace CodeBase.Off.Website.Mailers {
    using System.Net.Mail;

    using CodeBase.Off.Website.Models;

    public class EntriesMailer : CustomMailerBase {
        public virtual MailMessage PrepareEntry(EntryShowModel entry) {
            var from = new MailAddress(CurrentConfig.Mails.NoReply.Email,
                                       CurrentConfig.Mails.NoReply.DisplayName);

            var message = new MailMessage {
                    From = @from,
                    Subject = entry.Title,
                    IsBodyHtml = true
            };

            ViewData.Model = entry;

            PopulateBody(message,
                         "PrepareEntry");

            return message;
        }
    }
}