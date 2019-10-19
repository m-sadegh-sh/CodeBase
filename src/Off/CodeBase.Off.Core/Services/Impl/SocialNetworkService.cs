namespace CodeBase.Off.Core.Services.Impl {
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Off.Core.Domain;

    public sealed class SocialNetworkService : ISocialNetworkService {
        private readonly IRepository _repository;

        public SocialNetworkService(IRepository repository) {
            _repository = repository;
        }

        public bool Exists(string slug) {
            if (string.IsNullOrEmpty(slug))
                return false;

            return _repository.Exists<SocialNetwork>(slug);
        }

        public SocialNetwork Get(string slug) {
            if (string.IsNullOrEmpty(slug))
                return null;

            return _repository.Single<SocialNetwork>(slug);
        }

        public List<SocialNetwork> GetList() {
            return _repository.All<SocialNetwork>().ToList();
        }

        public void Save(SocialNetwork network) {
            network.Slug = network.Slug.ToLowerInvariant();

            _repository.Save(network);
        }

        public void Delete(string slug) {
            if (string.IsNullOrEmpty(slug))
                return;

            _repository.Delete<SocialNetwork>(slug);
        }
    }
}