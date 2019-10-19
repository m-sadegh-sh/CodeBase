namespace CodeBase.Off.Core.Services.Impl {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Off.Core.Domain;

    public sealed class TagService : ITagService {
        private readonly IRepository _repository;

        public TagService(IRepository repository) {
            _repository = repository;
        }

        public bool Exists(int id) {
            if (id < 1)
                return false;

            return _repository.Exists<Tag>(id);
        }

        public bool Exists(string title) {
            if (title.AsNullIfEmpty() == null)
                return false;

            return GetList().Any(t => t.Title == title);
        }

        public Tag Get(int id) {
            if (id < 1)
                return null;

            if (!Exists(id))
                return null;

            return _repository.Single<Tag>(id);
        }

        public Tag Get(string title) {
            if (title.AsNullIfEmpty() == null)
                return null;

            if (!Exists(title))
                return null;

            return GetList().First(t => t.Title == title);
        }

        public List<Tag> GetList() {
            return _repository.All<Tag>().ToList();
        }

        public int Save(Tag tag) {
            if(tag.Id==default(int))
            tag.Id = DateTime.UtcNow.Timestamp();

            _repository.Save(tag);

            return tag.Id;
        }

        public void Delete(int id) {
            if (id < 1)
                return;

            if (!Exists(id))
                return;

            _repository.Delete<Tag>(id);
        }

        public void Delete(string title) {
            if (title.AsNullIfEmpty() == null)
                return;

            if (!Exists(title))
                return;

            var tag = Get(title);

            _repository.Delete<Tag>(tag.Id);
        }
    }
}