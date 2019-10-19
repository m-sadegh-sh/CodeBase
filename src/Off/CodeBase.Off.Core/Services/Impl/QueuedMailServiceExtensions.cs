namespace CodeBase.Off.Core.Services.Impl {
    using System.Net.Mail;

    using CodeBase.Off.Core.Domain;

    public static class QueuedMailServiceExtensions {
        public static void Save(this IQueuedMailService service,
                                MailMessage message,
                                int accountConfigId) {
            var queuedMail = new QueuedMail {
                    From = message.From.Address,
                    FromName = message.From.DisplayName,
                    To = message.To[0].Address,
                    ToName = message.To[0].DisplayName,
                    Cc = string.Join(";",
                                     message.CC),
                    Bcc = string.Join(";",
                                      message.Bcc),
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = message.IsBodyHtml,
                    AccountConfigId = accountConfigId
            };

            service.Save(queuedMail);
        }
    }
}