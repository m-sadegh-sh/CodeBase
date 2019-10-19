namespace CodeBase.Off.Core.Services.Impl {
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Off.Core.Domain;

    public sealed class AttributeService : IAttributeService {
        private readonly IRepository _repository;

        public AttributeService(IRepository repository) {
            _repository = repository;
        }

        public bool Exists(object owner, string key) {
            return _repository.Exists<Attribute>(owner, key);
        }

        public Attribute Get(object owner, string key) {
            if (Exists(owner, key))
                return _repository.Single<Attribute>(owner, key);

            return null;
        }

        public TValue TryGetValue<TValue>(object owner, string key, TValue fallbackValue) {
            if (Exists(owner, key)) {
                var rawValue = _repository.Single<Attribute>(owner, key).Value;

                return (TValue) rawValue;
            }

            return fallbackValue;
        }

        public List<Attribute> GetList(object owner) {
            return _repository.All<Attribute>().Where(a => a.Owner.Equals(owner)).ToList();
        }

        public void Save(Attribute attribute) {
            _repository.Save(attribute);
        }

        public void Delete(object owner, string key) {
            _repository.Delete<Attribute>(owner, key);
        }

        public void DeleteAll() {
            _repository.DeleteAll<Attribute>();
        }
    }
}