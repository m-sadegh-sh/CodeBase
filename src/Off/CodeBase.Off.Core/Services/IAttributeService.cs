namespace CodeBase.Off.Core.Services {
    using System.Collections.Generic;

    using CodeBase.Off.Core.Domain;

    public interface IAttributeService {
        bool Exists(object owner, string key);
        Attribute Get(object owner, string key);
        TValue TryGetValue<TValue>(object owner, string key, TValue fallbackValue);
        List<Attribute> GetList(object owner);
        void Save(Attribute attribute);
        void Delete(object owner, string key);
        void DeleteAll();
    }
}