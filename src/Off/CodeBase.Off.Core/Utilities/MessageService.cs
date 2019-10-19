namespace CodeBase.Off.Core.Utilities {
    using System;
    using System.Net.Mail;

    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Core.Services.Impl;

    public sealed class MessageService : IMessageService {
        private readonly ILogService _logService;

        public MessageService(ILogService logService) {
            _logService = logService;
        }

        public bool SendEmail(MailMessage message) {
            try {
                new SmtpClient().Send(message);

                _logService.Info("A new message was sent.");

                return true;
            } catch (Exception exception) {
                _logService.Error("An error occured when sending a message", exception);

                return false;
            }
        }
    }
}