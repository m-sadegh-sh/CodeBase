namespace CodeBase.Off.Website.Jobs {
    using System;
    using System.Linq;
    using System.Net.Mail;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Core.Utilities;

    using Quartz;

    public sealed class NotSentQueuedMailsSenderJob : IJob {
        public const string Identity = "PENDING_QUEUEDMAILS_SENDER";

        public void Execute(IJobExecutionContext context) {
            var currentConfig = IoC.Get<Config>();
            var messageService = IoC.Get<IMessageService>();
            var queuedMailService = IoC.Get<IQueuedMailService>();
            var queuedMails = queuedMailService.GetList().
                                                Where(qm => !qm.SentDateUtc.HasValue && qm.SendingTries < 3).
                                                OrderByDescending(qm => qm.SendPriority);

            foreach (var queuedMail in queuedMails) {
                var bcc = string.IsNullOrWhiteSpace(queuedMail.Bcc) ? null : queuedMail.Bcc.Split(new[] {';'},
                                                                                                  StringSplitOptions.RemoveEmptyEntries);
                var cc = string.IsNullOrWhiteSpace(queuedMail.Cc) ? null : queuedMail.Cc.Split(new[] {';'},
                                                                                               StringSplitOptions.RemoveEmptyEntries);

                var from = new MailAddress(queuedMail.From,
                                           queuedMail.FromName);

                var to = new MailAddress(queuedMail.To,
                                         queuedMail.ToName);

                var message = new MailMessage(from,
                                              to) {
                                                      Subject = queuedMail.Subject,
                                                      Body = queuedMail.Body,
                                                      IsBodyHtml = queuedMail.IsBodyHtml
                                              };

                if (null != bcc) {
                    foreach (var address in bcc.Where(bccValue => !string.IsNullOrWhiteSpace(bccValue)))
                        message.Bcc.Add(address.Trim());
                }

                if (null != cc) {
                    foreach (var address in cc.Where(ccValue => !string.IsNullOrWhiteSpace(ccValue)))
                        message.CC.Add(address.Trim());
                }

                var accountConfig = currentConfig.Mails.GetAccountConfig(queuedMail.AccountConfigId);

                if (messageService.Send(message,
                                        accountConfig))
                    queuedMail.SentDateUtc = DateTime.UtcNow;

                queuedMail.SendingTries += 1;
                queuedMailService.Save(queuedMail);
            }
        }
    }
}