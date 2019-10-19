namespace CodeBase.Common.Web.Optimization.Bundling {
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Web.Optimization;

    using CodeBase.Common.Infrastructure.Application;

    using dotless.Core;
    using dotless.Core.Input;
    using dotless.Core.configuration;

    public class LessTransform<TFileReader> : IBundleTransform where TFileReader : IFileReader {
        public IDictionary<string, string> Tokens { get; set; }

        public void Process(BundleContext context, BundleResponse response) {
            var config = new DotlessConfiguration {
                MinifyOutput = false,
                ImportAllFilesAsLess = true,
                CacheEnabled = false,
                LessSource = typeof (TFileReader),
                Logger = typeof (LessLogger)
            };

            if (!Tokens.IsEmpty() && response.Content.AsNullIfEmpty() != null) {
                foreach (var token in Tokens)
                    response.Content = Regex.Replace(response.Content, token.Key, token.Value);
            }

            response.Content = Less.Parse(response.Content, config);
            response.ContentType = "text/css";
        }
    }
}