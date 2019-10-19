namespace CodeBase.Common.Infrastructure.Storage.Json {
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Web;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    
    public sealed class JsonPathProvider {
        private const string KEY_SEPARATOR = "%";

        private readonly string _basePath;
        private readonly string _baseFullPath;

        public JsonPathProvider(string basePath) {
            _basePath = basePath;

            var server = IoC.Get<HttpServerUtility>();
            _baseFullPath = server.MapPath(_basePath);
        }

        public string BasePath {
            get { return _basePath; }
        }

        public string GetFilePath<TEntity>() {
            var entityName = typeof (TEntity).Name;

            var folderName = entityName.InflectTo().Pluralized;

            var filePath = Path.Combine(_baseFullPath, folderName);

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            return filePath;
        }

        public string GetFileFullPath<TEntity>(IEnumerable<object> keys) {
            var filePath = GetFilePath<TEntity>();

            var fileName = GetFileNameFromKeys(keys);

            var fileFullPath = Path.Combine(filePath, fileName + ".json");

            return fileFullPath;
        }

        private static string GetFileNameFromKeys(IEnumerable<object> keys) {
            var keySequence = new StringBuilder();
            foreach (var key in keys)
                keySequence.Append(key + KEY_SEPARATOR);

            var fileName = keySequence.ToString();

            return fileName.Substring(0, fileName.Length - KEY_SEPARATOR.Length);
        }

        public string[] GetFileFullPaths<TEntity>() {
            var filePath = GetFilePath<TEntity>();

            var fileFullPaths = Directory.GetFiles(filePath, "*.json", SearchOption.TopDirectoryOnly);

            return fileFullPaths;
        }
    }
}