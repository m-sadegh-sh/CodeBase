namespace CodeBase.Off.Core.Services.Impl {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Off.Core.Domain;

    public sealed class EntryService : IEntryService {
        private readonly IUserService _userService;
        private readonly IRepository _repository;

        public EntryService(IUserService userService, IRepository repository) {
            _userService = userService;
            _repository = repository;
        }

        public bool Exists(string slug) {
            if (string.IsNullOrEmpty(slug))
                return false;

            return _repository.Exists<Entry>(slug);
        }

        public Entry Get(string slug) {
            if (string.IsNullOrEmpty(slug))
                return null;

            return _repository.Single<Entry>(slug);
        }

        public List<Entry> GetList() {
            return _repository.All<Entry>().ToList();
        }

        public void Save(Entry entry) {
            entry.Slug = entry.Slug.ToLowerInvariant();

            if (entry.CreateDateUtc == default(DateTime)) {
                var isUpdate = _repository.Exists<Entry>(entry.Slug);

                if (isUpdate) {
                    var oldEntry = _repository.Single<Entry>(entry.Slug);
                    entry.CreateDateUtc = oldEntry.CreateDateUtc;
                } else
                    entry.CreateDateUtc = DateTime.UtcNow;
            }

            entry.AuthorId = _userService.Current.Id;

            _repository.Save(entry);
        }

        public void Delete(string slug) {
            if (string.IsNullOrEmpty(slug))
                return;

            _repository.Delete<Entry>(slug);
        }
    }
}