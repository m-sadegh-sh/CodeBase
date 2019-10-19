namespace CodeBase.Off.Core.Services.Impl {
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Off.Core.Domain;

    public sealed class PortfolioService : IPortfolioService {
        private readonly IRepository _repository;

        public PortfolioService(IRepository repository) {
            _repository = repository;
        }

        public bool Exists(string slug) {
            if (string.IsNullOrEmpty(slug))
                return false;

            return _repository.Exists<Portfolio>(slug);
        }

        public Portfolio Get(string slug) {
            if (string.IsNullOrEmpty(slug))
                return null;

            return _repository.Single<Portfolio>(slug);
        }

        public List<Portfolio> GetList() {
            return _repository.All<Portfolio>().ToList();
        }

        public void Save(Portfolio portfolio) {
            portfolio.Slug = portfolio.Slug.ToLowerInvariant();

            _repository.Save(portfolio);
        }

        public void Delete(string slug) {
            if (string.IsNullOrEmpty(slug))
                return;

            _repository.Delete<Portfolio>(slug);
        }
    }
}