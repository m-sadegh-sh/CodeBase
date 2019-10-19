namespace CodeBase.Common.Infrastructure.Application {
    using System.Web;

    using CodeBase.Common.Infrastructure.DependencyResolution;

    public static class StringExtensions {
        public static string AsNullIfEmpty(this string value) {
            return string.IsNullOrEmpty(value) ? null : value;
        }

        public static string AsNullIfWhiteSpace(this string value) {
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        public static string ToFullPath(this string value) {
            var server = IoC.Get<HttpServerUtility>();

            return server.MapPath((!value.StartsWith("~/") ? "~/" : null) + value);
        }
    }
}