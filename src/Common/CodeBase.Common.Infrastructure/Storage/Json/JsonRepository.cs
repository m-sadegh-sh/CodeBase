namespace CodeBase.Common.Infrastructure.Storage.Json {
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;

    public sealed class JsonRepository : IRepository {
        private readonly JsonKeyProvider _keyProvider;
        private readonly JsonPathProvider _pathProvider;

        public JsonRepository(JsonKeyProvider keyProvider, JsonPathProvider pathProvider) {
            _keyProvider = keyProvider;
            _pathProvider = pathProvider;
        }

        public bool Exists<TEntity>(params object[] keys) {
            var fullFilePath = _pathProvider.GetFileFullPath<TEntity>(keys);

            return File.Exists(fullFilePath);
        }

        public TEntity Single<TEntity>(params object[] keys) {
            if (!Exists<TEntity>(keys))
                return default(TEntity);

            var filePath = _pathProvider.GetFileFullPath<TEntity>(keys);

            var jsonString = File.ReadAllText(filePath);

            var entity = JsonConvert.DeserializeObject<TEntity>(jsonString);

            return entity;
        }

        public IQueryable<TEntity> All<TEntity>() {
            var filePaths = _pathProvider.GetFileFullPaths<TEntity>();

            var entities = filePaths.Select(File.ReadAllText).Select(JsonConvert.DeserializeObject<TEntity>).ToList();

            return entities.AsQueryable();
        }

        public void Save<TEntity>(TEntity entity) {
            var jsonString = JsonConvert.SerializeObject(entity, Formatting.Indented);

            var fileName = _keyProvider.GetAllKeyValues(entity);

            var fullFilePath = _pathProvider.GetFileFullPath<TEntity>(fileName);

            File.WriteAllText(fullFilePath, jsonString);
        }

        public void Delete<TEntity>(params object[] keys) {
            var fullFilePath = _pathProvider.GetFileFullPath<TEntity>(keys);

            if (File.Exists(fullFilePath))
                File.Delete(fullFilePath);
        }

        public void DeleteAll<TEntity>() {
            var filePath = _pathProvider.GetFilePath<TEntity>();

            if (Directory.Exists(filePath))
                Directory.Delete(filePath, true);
        }
    }
}