namespace CodeBase.Off.Core.Services {
    using System.Collections.Generic;

    using CodeBase.Off.Core.Domain;

    public interface IResourceHitService {
        bool Exists(int id);
        bool Exists(string url);
        ResourceHit Get(int id);
        ResourceHit Get(string url);
        IList<ResourceHit> GetList();
        void Save(ResourceHit hit);
        void Delete(int id);
        void Delete(string url);
    }
}