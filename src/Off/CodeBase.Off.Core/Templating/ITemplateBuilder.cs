namespace CodeBase.Off.Core.Templating {
    using System.Net.Mail;
    
    public interface ITemplateBuilder<in T> {
        void Build(T entity, MailMessage message);
    }
}