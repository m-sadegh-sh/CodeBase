namespace CodeBase.Off.Core.Services.Impl {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Off.Core.Domain;

    public sealed class QueuedMailService : IQueuedMailService {
        private readonly IRepository _repository;

        public QueuedMailService(IRepository repository) {
            _repository = repository;
        }

        public bool Exists(int id) {
            if (id < 1)
                return false;

            return _repository.Exists<QueuedMail>(id);
        }

        public QueuedMail Get(int id) {
            if (id < 1)
                return null;

            if (!Exists(id))
                return null;

            return _repository.Single<QueuedMail>(id);
        }

        public IList<QueuedMail> GetList() {
            return _repository.All<QueuedMail>().
                               ToList();
        }

        public void Save(QueuedMail mail) {
            if (mail.Id == default(int))
                mail.Id = DateTime.UtcNow.Timestamp();

            _repository.Save(mail);
        }

        public void Delete(int id) {
            if (id < 1)
                return;

            if (!Exists(id))
                return;

            _repository.Delete<QueuedMail>(id);
        }
    }
}