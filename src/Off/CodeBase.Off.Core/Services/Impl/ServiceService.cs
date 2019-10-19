namespace CodeBase.Off.Core.Services.Impl {
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Off.Core.Domain;

    public sealed class ServiceService : IServiceService {
        private readonly IRepository _repository;

        public ServiceService(IRepository repository) {
            _repository = repository;
        }

        public bool Exists(string slug) {
            if (string.IsNullOrEmpty(slug))
                return false;

            return _repository.Exists<Service>(slug);
        }

        public Service Get(string slug) {
            if (string.IsNullOrEmpty(slug))
                return null;

            return _repository.Single<Service>(slug);
        }

        public List<Service> GetList() {
            return _repository.All<Service>().ToList();
        }

        public void Save(Service service) {
            service.Slug = service.Slug.ToLowerInvariant();

            _repository.Save(service);
        }

        public void Delete(string slug) {
            if (string.IsNullOrEmpty(slug))
                return;

            _repository.Delete<Service>(slug);
        }
    }
}