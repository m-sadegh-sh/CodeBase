namespace CodeBase.Off.Core.Services.Impl {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Off.Core.Domain;

    public sealed class HitInfoService : IHitInfoService {
        private readonly IRepository _repository;

        public HitInfoService(IRepository repository) {
            _repository = repository;
        }

        public bool Exists(int id) {
            if (id < 1)
                return false;

            return _repository.Exists<Tag>(id);
        }

        public bool IsResolvable(int hitId,
                                 string requestIp) {
            if (hitId < 1)
                return false;

            if (requestIp.AsNullIfEmpty() == null)
                return false;

            var info = GetList().
                    FirstOrDefault(hi => hi.HitId == hitId && hi.RequestIp == requestIp);

            var isResolvable = info == null || info.HitDateUtc < DateTime.UtcNow.AddSeconds(30);

            return isResolvable;
        }

        public HitInfo Get(int id) {
            if (id < 1)
                return null;

            if (!Exists(id))
                return null;

            return _repository.Single<HitInfo>(id);
        }

        public IList<HitInfo> GetList() {
            return _repository.All<HitInfo>().
                               ToList();
        }

        public void Save(HitInfo info) {
            if (info.Id == default(int))
                info.Id = DateTime.UtcNow.Timestamp();

            _repository.Save(info);
        }

        public void Delete(int id) {
            if (id < 1)
                return;

            if (!Exists(id))
                return;

            _repository.Delete<HitInfo>(id);
        }
    }
}