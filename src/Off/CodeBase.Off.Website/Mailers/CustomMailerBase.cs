namespace CodeBase.Off.Website.Mailers {
    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;

    using Mvc.Mailer;

    public abstract class CustomMailerBase : MailerBase {
        protected CustomMailerBase() {
            MasterName = "~/Views/Shared/Layouts/_MailLayoutBase.cshtml";

            LogService = IoC.Get<ILogService>();
            CurrentUser = IoC.Get<User>();
            CurrentConfig = IoC.Get<Config>();
        }

        protected ILogService LogService { get; set; }

        protected User CurrentUser { get; private set; }

        protected Config CurrentConfig { get; private set; }
    }
}