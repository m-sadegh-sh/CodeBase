namespace CodeBase.Off.Website.Jobs {
    using System.Linq;
    using System.Net.Mail;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Core.Templating;
    using CodeBase.Off.Core.Utilities;

    using Quartz;

    public sealed class EntriesSenderJob : IJob {
        public const string Identity = "ENTRIES_SENDER";

        public void Execute(IJobExecutionContext context) {
            var config = IoC.Get<Config>();
            var entryService = IoC.Get<IEntryService>();
            var entryTemplateBuilder = IoC.Get<ITemplateBuilder<Entry>>();
            var subscriptionService = IoC.Get<ISubscriptionService>();
            var messageService = IoC.Get<IMessageService>();

            var entries = entryService.GetList().Where(e => !e.WasSent).ToList();
            var subscriptions = subscriptionService.GetList().Where(s => s.IsConfirmed).ToList();

            var from = new MailAddress(config.Mails.Newsletter);

            foreach (var entry in entries) {
                var message = new MailMessage {
                    From = from
                };

                entryTemplateBuilder.Build(entry, message);

                foreach (var subscription in subscriptions)
                    message.Bcc.Add(subscription.Email);

                messageService.SendEmail(message);

                entry.WasSent = true;
                entryService.Save(entry);
            }
        }
    }
}