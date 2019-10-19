namespace CodeBase.Off.Core.Services.Impl {
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Off.Core.Domain;

    public sealed class FriendLinkService : IFriendLinkService {
        private readonly IRepository _repository;

        public FriendLinkService(IRepository repository) {
            _repository = repository;
        }

        public bool Exists(string slug) {
            if (string.IsNullOrEmpty(slug))
                return false;

            return _repository.Exists<FriendLink>(slug);
        }

        public FriendLink Get(string slug) {
            if (string.IsNullOrEmpty(slug))
                return null;

            return _repository.Single<FriendLink>(slug);
        }

        public List<FriendLink> GetList() {
            return _repository.All<FriendLink>().ToList();
        }

        public void Save(FriendLink link) {
            link.Slug = link.Slug.ToLowerInvariant();

            _repository.Save(link);
        }

        public void Delete(string slug) {
            if (string.IsNullOrEmpty(slug))
                return;

            _repository.Delete<FriendLink>(slug);
        }
    }
}