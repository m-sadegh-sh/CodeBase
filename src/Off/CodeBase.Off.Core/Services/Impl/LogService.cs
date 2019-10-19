namespace CodeBase.Off.Core.Services.Impl {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Off.Core.Domain;

    public sealed class LogService : ILogService {
        private readonly IRepository _repository;

        public LogService(IRepository repository) {
            _repository = repository;
        }

        public bool Exists(int id) {
            return _repository.Exists<Log>(id);
        }

        public Log Get(int id) {
            return _repository.Single<Log>(id);
        }

        public List<Log> GetList() {
            return _repository.All<Log>().ToList();
        }

        public int Save(Log log) {
            if(log.Id==default(int))
            log.Id = DateTime.UtcNow.Timestamp();

            _repository.Save(log);

            return log.Id;
        }

        public void Delete(int id) {
            _repository.Delete<Log>(id);
        }
    }
}