namespace CodeBase.Off.Core.Utilities {
    using System.Net.Mail;

    public interface IMessageService {
        bool SendEmail(MailMessage message);
    }
}