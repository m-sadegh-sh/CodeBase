namespace CodeBase.Off.Website.Configurators {
    using System;
    using System.Web;
    using System.Web.Mvc;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Common.Web.Routing;
    using CodeBase.Off.Core.Web.Mvc;

    public static class BundlingHelper {
        public static string AsLess(this string bundlePath, bool template, bool appendExtension = true) {
            return AsCore(h => h.Less(bundlePath, template, appendExtension));
        }

        public static string AsStyle(this string bundlePath, bool template, bool appendExtension = true) {
            return AsCore(h => h.Style(bundlePath, template, appendExtension));
        }

        public static string AsScript(this string bundlePath, bool template, bool appendExtension = true) {
            return AsCore(h => h.Script(bundlePath, template, appendExtension));
        }

        private static string AsCore(Func<UrlHelper, string> generator) {
            var helper = IoC.Get<HttpContext>().Url();

            var fotmattedPath = string.Format("~{0}", generator(helper));

            return fotmattedPath;
        }
    }
}