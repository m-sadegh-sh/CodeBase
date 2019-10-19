namespace CodeBase.Off.Core.Services.Impl {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Off.Core.Domain;

    public sealed class ResourceHitService : IResourceHitService {
        private readonly IRepository _repository;

        public ResourceHitService(IRepository repository) {
            _repository = repository;
        }

        public bool Exists(int id) {
            if (id < 1)
                return false;

            return _repository.Exists<ResourceHit>(id);
        }

        public bool Exists(string url) {
            if (url.AsNullIfEmpty() == null)
                return false;

            return GetList().
                    Any(rh => rh.Url == url);
        }

        public ResourceHit Get(int id) {
            if (id < 1)
                return null;

            if (!Exists(id))
                return null;

            return _repository.Single<ResourceHit>(id);
        }

        public ResourceHit Get(string url) {
            if (url.AsNullIfEmpty() == null)
                return null;

            if (!Exists(url))
                return null;

            return GetList().
                    First(rh => rh.Url == url);
        }

        public IList<ResourceHit> GetList() {
            return _repository.All<ResourceHit>().
                               ToList();
        }

        public void Save(ResourceHit hit) {
            if (hit.Id == default(int))
                hit.Id = DateTime.UtcNow.Timestamp();

            _repository.Save(hit);
        }

        public void Delete(int id) {
            if (id < 1)
                return;

            if (!Exists(id))
                return;

            _repository.Delete<ResourceHit>(id);
        }

        public void Delete(string url) {
            if (url.AsNullIfEmpty() == null)
                return;

            if (!Exists(url))
                return;

            var hit = Get(url);

            _repository.Delete<ResourceHit>(hit.Id);
        }
    }
}