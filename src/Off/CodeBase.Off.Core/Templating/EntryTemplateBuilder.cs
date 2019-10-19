namespace CodeBase.Off.Core.Templating {
    using System.Net.Mail;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Domain;

    using MarkdownSharp;

    public sealed class EntryTemplateBuilder : ITemplateBuilder<Entry> {
        public void Build(Entry entity, MailMessage message) {
            var markdown = IoC.Get<Markdown>();

            message.Subject = entity.Title;
            message.Body = markdown.Transform(entity.FullStory);
        }
    }
}