namespace CodeBase.Off.Core.Services {
    using System.Collections.Generic;

    using CodeBase.Off.Core.Domain;

    public interface ITagService {
        bool Exists(int id);
        bool Exists(string title);
        Tag Get(int id);
        Tag Get(string title);
        List<Tag> GetList();
        int Save(Tag tag);
        void Delete(int id);
        void Delete(string title);
    }
}