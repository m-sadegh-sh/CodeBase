namespace CodeBase.Off.Website.Configurators {
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web.Hosting;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Domain;

    using dotless.Core.Input;

    public sealed class VirtualFileReader : IFileReader {
        private readonly IDictionary<string, string> _tokens;

        public VirtualFileReader() {
            var config = IoC.Get<Config>();

            _tokens = new Dictionary<string, string> {
                {"%Template%", config.Template},
                {"%CdnUrl%", config.Seo.CdnUrl},
                {"%ImagesPath%", config.Seo.ImagesPath},
                {"%StylesPath%", config.Seo.StylesPath},
                {"%ScriptsPath%", config.Seo.ScriptsPath},
                {"%FontsPath%", config.Seo.FontsPath}
            };
        }

        public byte[] GetBinaryFileContents(string fileName) {
            fileName = GetFullPath(fileName);

            return File.ReadAllBytes(fileName);
        }

        public string GetFileContents(string fileName) {
            fileName = GetFullPath(fileName);

            var fileContents = File.ReadAllText(fileName);

            if (!_tokens.IsEmpty() && fileContents.AsNullIfEmpty() != null) {
                foreach (var token in _tokens)
                    fileContents = Regex.Replace(fileContents, token.Key, token.Value);
            }

            return fileContents;
        }

        public bool DoesFileExist(string fileName) {
            fileName = GetFullPath(fileName);

            return File.Exists(fileName);
        }

        private static string GetFullPath(string fileName) {
            var template = fileName.Contains("-t");
            fileName = fileName.AsLess(template);

            return HostingEnvironment.MapPath(fileName);
        }
    }
}