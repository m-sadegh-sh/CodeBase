namespace CodeBase.Off.Core.Services.Impl {
    using System;
    using System.Web;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Common.Web.Extensions;
    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.LogActionProviders;

    public static class LogServiceExtensions {
        public static void Debug(this ILogService service, string message) {
            service.Save(Log.SeverityLevel.Crud, null, message, null);
        }

        public static void Info(this ILogService service, string message) {
            service.Save(Log.SeverityLevel.Info, null, message, null);
        }

        public static void Warning(this ILogService service, string message) {
            service.Save(Log.SeverityLevel.Warning, null, message, null);
        }

        public static void Warning(this ILogService service, Exception exception) {
            service.Save(Log.SeverityLevel.Warning, null, null, exception.ToFriendlyString());
        }

        public static void Warning(this ILogService service, string message, Exception exception) {
            service.Save(Log.SeverityLevel.Warning, null, message, exception.ToFriendlyString());
        }

        public static void Error(this ILogService service, string message) {
            service.Save(Log.SeverityLevel.Error, null, message, null);
        }

        public static void Error(this ILogService service, Exception exception) {
            service.Save(Log.SeverityLevel.Error, null, null, exception.ToFriendlyString());
        }

        public static void Error(this ILogService service, string message, Exception exception) {
            service.Save(Log.SeverityLevel.Error, null, message, exception.ToFriendlyString());
        }

        public static void Crud<TLogActionProvider, T>(this ILogService service, T old, T @new) where TLogActionProvider : LogActionProviderBase<T>, new() where T : class {
            var logActionProvider = new TLogActionProvider();

            var action = logActionProvider.Build(old, @new);

            service.Save(Log.SeverityLevel.Crud, action, null, null);
        }

        private static void Save(this ILogService service, Log.SeverityLevel level, LogAction action, string message, string exception) {
            var request = IoC.Get<HttpRequest>();

            var currentUser = IoC.Get<User>();

            var log = new Log {
                Level = level,
                LogDateUtc = DateTime.UtcNow,
                RawUrl = request.RawUrl,
                ReferrerUrl = request.UrlReferrer.ToStringOrNull(),
                IpInfo = string.Format("Remote: {0}, Local: {1}", request.RemoteIp(), request.LocalIp()),
                Params = request.LoggableParams(),
                Action = action,
                UserInfo = string.Format("{0} ({1})", currentUser.UserName.AsNullIfEmpty() ?? "---", currentUser.FriendlyName.AsNullIfWhiteSpace() ?? "---"),
                Message = message,
                Exception = exception
            };

            service.Save(log);
        }
    }
}