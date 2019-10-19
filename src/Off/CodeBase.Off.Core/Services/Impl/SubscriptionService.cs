namespace CodeBase.Off.Core.Services.Impl {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Off.Core.Domain;

    public sealed class SubscriptionService : ISubscriptionService {
        private readonly IRepository _repository;

        public SubscriptionService(IRepository repository) {
            _repository = repository;
        }

        public bool Exists(Guid guid) {
            if (guid == Guid.Empty)
                return false;

            return _repository.Exists<Subscription>(guid);
        }

        public bool Exists(string email) {
            if (email.AsNullIfEmpty() == null)
                return false;

            return GetList().Any(s => s.Email == email);
        }

        public Subscription Get(Guid guid) {
            if (guid == Guid.Empty)
                return null;

            if (!Exists(guid))
                return null;

            return _repository.Single<Subscription>(guid);
        }

        public Subscription Get(string email) {
            if (email.AsNullIfEmpty() == null)
                return null;

            if (!Exists(email))
                return null;

            return GetList().First(s => s.Email == email);
        }

        public List<Subscription> GetList() {
            return _repository.All<Subscription>().ToList();
        }

        public Guid Save(Subscription subscription) {
            if (subscription.Guid == Guid.Empty)
                subscription.Guid = Guid.NewGuid();

            _repository.Save(subscription);

            return subscription.Guid;
        }

        public void Delete(Guid guid) {
            if (guid == Guid.Empty)
                return;

            if (!Exists(guid))
                return;

            _repository.Delete<Subscription>(guid);
        }

        public void Delete(string email) {
            if (email.AsNullIfEmpty() == null)
                return;

            if (!Exists(email))
                return;

            var subscription = Get(email);

            _repository.Delete<Guid>(subscription.Guid);
        }
    }
}