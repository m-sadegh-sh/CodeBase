namespace CodeBase.Off.Core.Services {
    using System.Collections.Generic;

    using CodeBase.Off.Core.Domain;

    public interface IQueuedMailService {
        bool Exists(int id);
        QueuedMail Get(int id);
        IList<QueuedMail> GetList();
        void Save(QueuedMail mail);
        void Delete(int id);
    }
}