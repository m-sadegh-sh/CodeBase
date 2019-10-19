namespace CodeBase.Off.Core.Services {
    using System.Collections.Generic;

    using CodeBase.Off.Core.Domain;

    public interface ILogService {
        bool Exists(int id);
        Log Get(int id);
        List<Log> GetList();
        int Save(Log log);
        void Delete(int id);
    }
}