namespace CodeBase.Off.Core.Services {
    using System;
    using System.Collections.Generic;

    using CodeBase.Off.Core.Domain;

    public interface ISubscriptionService {
        bool Exists(Guid guid);
        bool Exists(string email);
        Subscription Get(Guid guid);
        Subscription Get(string email);
        List<Subscription> GetList();
        Guid Save(Subscription subscription);
        void Delete(Guid guid);
        void Delete(string email);
    }
}