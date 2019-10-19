namespace CodeBase.Off.Core.Web.Mvc {
    using System.Web.Mvc;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Domain;

    public static class UrlHelperExtensions {
        public static string Image(this UrlHelper helper, string fileName, bool template = false) {
            var config = IoC.Get<Config>();

            return helper.Cdn(template, config.Seo.ImagesPath, fileName);
        }

        public static string Less(this UrlHelper helper, string fileName, bool template = false, bool appendExtension = true) {
            var config = IoC.Get<Config>();

            return helper.Cdn(template, config.Seo.LessPath, fileName, appendExtension ? ".less" : null);
        }

        public static string Style(this UrlHelper helper, string fileName, bool template = false, bool appendExtension = true) {
            var config = IoC.Get<Config>();

            return helper.Cdn(template, config.Seo.StylesPath, fileName, appendExtension ? ".css" : null);
        }

        public static string Script(this UrlHelper helper, string fileName, bool template = false, bool appendExtension = true) {
            var config = IoC.Get<Config>();

            return helper.Cdn(template, config.Seo.ScriptsPath, fileName, appendExtension ? ".js" : null);
        }

        public static string Font(this UrlHelper helper, string fileName, bool template = false) {
            var config = IoC.Get<Config>();

            return helper.Cdn(template, config.Seo.FontsPath, fileName);
        }

        public static string Cdn(this UrlHelper helper, bool template = false, params string[] parts) {
            var config = IoC.Get<Config>();

            return (config.Seo.CdnUrl + (template ? ("templates/" + config.Template + "/") : "") + string.Join("", parts)).ToLowerInvariant();
        }
    }
}