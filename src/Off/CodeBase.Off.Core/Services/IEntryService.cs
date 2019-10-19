namespace CodeBase.Off.Core.Services {
    using System.Collections.Generic;

    using CodeBase.Off.Core.Domain;

    public interface IEntryService {
        bool Exists(string slug);
        Entry Get(string slug);
        List<Entry> GetList();
        void Save(Entry entry);
        void Delete(string slug);
    }
}