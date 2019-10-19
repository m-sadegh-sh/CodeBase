namespace CodeBase.Off.Website.Configurators {
    using System.Collections.Generic;
    using System.Web.Optimization;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Common.Web.Optimization.Bundling;
    using CodeBase.Off.Core.Domain;

    public static class BundleRegisterar {
        public static void RegisterBundles() {
            BundleTable.EnableOptimizations = true;
            var bundles = BundleTable.Bundles;

            var config = IoC.Get<Config>();

            var lessTransform = new LessTransform<VirtualFileReader> {
                Tokens = new Dictionary<string, string> {
                    {"%Template%", config.Template},
                    {"%CdnUrl%", config.Seo.CdnUrl},
                    {"%ImagesPath%", config.Seo.ImagesPath},
                    {"%StylesPath%", config.Seo.StylesPath},
                    {"%ScriptsPath%", config.Seo.ScriptsPath},
                    {"%FontsPath%", config.Seo.FontsPath}
                }
            };

            bundles.Add(new Bundle("Plugins".AsStyle(false, false)).Include(
                "Variables".AsLess(false),
                "Mixins".AsLess(false),
                "jQuery-Fancybox".AsLess(false),
                "Fancybox-Helpers/Buttons".AsLess(false),
                "Fancybox-Helpers/Thumbs".AsLess(false),
                "jQuery-Nivo-Slider".AsLess(false),
                "Wmd".AsLess(false)).WithTransform(lessTransform)
                            .WithTransform<CssMinify>());

            bundles.Add(new Bundle("Syntax-Highlighter".AsStyle(false, false)).Include(
                "Syntax-Highlighter-Core".AsLess(false),
                "Brushes/Default".AsLess(false)).WithTransform(lessTransform)
                            .WithTransform<CssMinify>());

            bundles.Add(new Bundle("All".AsStyle(true, false)).Include(
                "Variables".AsLess(false),
                "Mixins".AsLess(false),
                "Reset".AsLess(false),
                "Utilities".AsLess(false),
                "Core".AsLess(true),
                "Header".AsLess(true),
                "Content".AsLess(true),
                "Section".AsLess(true),
                "Slider".AsLess(true),
                "Social".AsLess(true),
                "Footer".AsLess(true),
                "Forms".AsLess(true),
                "Bootstrap-Alerts".AsLess(true)).WithTransform(lessTransform)
                            .WithTransform<CssMinify>());

            bundles.Add(new ScriptBundle("Modernizr".AsScript(false, false)).Include(
                "Modernizr".AsScript(false)));

            bundles.Add(new ScriptBundle("jQuery".AsScript(false, false)).Include(
                "jQuery".AsScript(false)));

            bundles.Add(new ScriptBundle("jQuery-Plugins".AsScript(false, false)).Include(
                "jQuery.HoverIntent".AsScript(false),
                "jQuery-Easing".AsScript(false),
                "jQuery.MouseWheel".AsScript(false),
                "Fancybox-Helpers/Buttons".AsScript(false),
                "Fancybox-Helpers/Media".AsScript(false),
                "Fancybox-Helpers/Thumbs".AsScript(false),
                "jQuery-Fancybox".AsScript(false),
                "jQuery-Nivo-Slider".AsScript(false),
                "jQuery-Smooth-Scroll".AsScript(false),
                "jQuery-Watermark".AsScript(false),
                "jQuery-Wmd".AsScript(false),
                "Bootstrap-Alert".AsScript(false)));

            bundles.Add(new ScriptBundle("jQuery-Validation".AsScript(false, false)).Include(
                "jQuery-Validate".AsScript(false),
                "jQuery-Validate-Unobtrusive".AsScript(false),
                "jQuery-Unobtrusive-Ajax".AsScript(false)));

            bundles.Add(new ScriptBundle("Syntax-Highlighter".AsScript(false, false)).Include(
                "Syntax-Highlighter-Core".AsScript(false),
                "Languages/Java-Script".AsScript(false) /*,
                "Languages/Plain".AsScript(false),
                "Languages/Css".AsScript(false),
                "Languages/C-Sharp".AsScript(false),
                "Languages/Power-Shell".AsScript(false),
                "Languages/Xml".AsScript(false),
                "Languages/Sql".AsScript(false)*/));

            bundles.Add(new ScriptBundle("All".AsScript(true, false)).Include(
                "Init".AsScript(true)));
        }
    }
}